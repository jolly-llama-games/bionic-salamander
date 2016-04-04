using UnityEngine;
using System.Collections;

public class CatcherScript : MonoBehaviour {

    private PlayerController player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
    }
	
	void FixedUpdate () {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
