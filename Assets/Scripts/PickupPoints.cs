using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour {

    public int value;

    private ScoreManager scoreManager;

    public delegate void CoinEvents ();
    public static event CoinEvents OnPickup;

    // Use this for initialization
    void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger OnPickup event
            if (OnPickup != null)
                OnPickup();
            scoreManager.AddScore(value);
            gameObject.SetActive(false);
        }
    }
}
