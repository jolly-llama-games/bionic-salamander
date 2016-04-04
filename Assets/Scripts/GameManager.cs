using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    private PlayerController player;
    private ScoreManager scoreManager;

    public HighScoreFlag hiScoreFlag;

    public ObjectPooler[] objectPoolers;

    public DeathMenu deathMenu;

    public bool resetting = false;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        platformStartPoint = platformGenerator.position;
        hiScoreFlag.Move(scoreManager.hiScore, scoreManager.highScorePos);
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.R) && !resetting)
        {
            RestartGame();
        }
	}

    public void RestartGame ()
    {
        resetting = true;
        scoreManager.scoreIncreasing = false;
        player.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);

        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        deathMenu.gameObject.SetActive(false);
        platformGenerator.position = platformStartPoint;
        /*
        for (int i = 0; i < objectPoolers.Length; i++)
        {
            objectPoolers[i].Reset();
        }
        */


        // Reset all resettable objects, including ObjectPools and the PlayerController.
        System.Collections.Generic.IList<IResettable> resettables = InterfaceHelper.FindObjects<IResettable>();

        foreach (var resettable in resettables)
        {
            Debug.Log(resettable);
            resettable.Reset();
        }
        
        hiScoreFlag.Move(scoreManager.hiScore, scoreManager.highScorePos);
        player.Reset(); // <-- should not be necessary, but is?
        player.gameObject.SetActive(true);

        scoreManager.score = 0;
        scoreManager.scoreIncreasing = true;
        resetting = false;
    }

    /*
    public IEnumerator RestartGameCo ()
    {
        scoreManager.scoreIncreasing = false;
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformGenerator.position = platformStartPoint;
        for (int i = 0; i < objectPoolers.Length; i++)
        {
            objectPoolers[i].RecallAllPooledObjects();
        }
        hiScoreFlag.Move(scoreManager.hiScore, scoreManager.highScorePos);
        player.ResetPlayer();
        player.gameObject.SetActive(true);

        scoreManager.score = 0;
        scoreManager.scoreIncreasing = true;
    }*/
}
