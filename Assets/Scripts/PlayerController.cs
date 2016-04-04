using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IResettable {

    public float moveSpeed = 5;
    public float maxMoveSpeed = 12;
    public float speedMultiplier;
    public float speedIncreaseMilestone;

    private float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;
    private float moveSpeedStore;
    private Vector3 startPoint;

    public float jumpForce = 15;

    public float jumpTime = 2;
    private float jumpTimeCounter;

    private Rigidbody2D rb2d;

    private bool stoppedJumping;

    private bool isGrounded;
    public LayerMask groundLayers;
    public Transform groundCheck;
    public float groundCheckRadius;

    public int jumpLimit = 1;
    private int jumpsLeft;

    private Animator animator;
    public GameManager gameManager;

    public delegate void PlayerEvents();
    public static event PlayerEvents OnJump;
    public static event PlayerEvents OnDeath;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        stoppedJumping = true;
        jumpsLeft = jumpLimit;


        startPoint = transform.position;
        speedMilestoneCountStore = speedMilestoneCount;
        moveSpeedStore = moveSpeed;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
    }

    #if UNITY_EDITOR
    void OnDrawGizmos ()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(groundCheck.position, Vector3.back, groundCheckRadius);
    }
    #endif

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers) && rb2d.velocity.y <= 0;
    }

	// Update is called once per frame
	void Update () {

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone *= speedMultiplier;

            moveSpeed *= speedMultiplier;
            moveSpeed = Mathf.Min(moveSpeed, maxMoveSpeed);
        }

        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                if (isGrounded)
                {
                    // Regular jump
                }
                else
                {
                    // Multi-jump
                }
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                // Set isGrounded to false to ensure the isGrounded check below doesn't run this Update
                isGrounded = false;
                // Trigger OnJump event
                if (OnJump != null)
                    OnJump();
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping && !EventSystem.current.IsPointerOverGameObject())
        {
            if (jumpTimeCounter > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && !EventSystem.current.IsPointerOverGameObject())
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (isGrounded)
        {
            jumpTimeCounter = jumpTime;
            jumpsLeft = jumpLimit;
        }

        animator.SetFloat("Speed", rb2d.velocity.x);
        animator.SetBool("Grounded", isGrounded);
	}

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("killbox"))
        {
            // Trigger OnDeath event
            if (OnDeath != null)
                OnDeath();
            gameManager.RestartGame();
        }
    }

    public void Reset ()
    {
        transform.position = startPoint;
        moveSpeed = moveSpeedStore;
        jumpsLeft = jumpLimit;
        jumpTimeCounter = jumpTime;
        stoppedJumping = true;
        speedMilestoneCount = speedMilestoneCountStore;
        speedIncreaseMilestone = speedIncreaseMilestoneStore;
    }
}
