using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    public bool doublePoints;
    public bool safeMode;

    public float powerupDuration;

    public Powerup ()
    {
        // Constructor
    }

    public virtual void Activate ()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Activate();
            gameObject.SetActive(false);
        }
    }
}
