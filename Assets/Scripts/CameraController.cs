using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private PlayerController player;

    private Vector3 lastPlayerPos;
    private float distanceToMove;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        lastPlayerPos = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        distanceToMove = player.transform.position.x - lastPlayerPos.x;

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPos = player.transform.position;
	}
}
