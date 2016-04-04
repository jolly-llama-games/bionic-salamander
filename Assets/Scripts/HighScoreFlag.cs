using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreFlag : MonoBehaviour {

    public Text hiScoreFlagText;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        hiScoreFlagText.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
    }

    public void Move (float newScore, Vector3 highScorePos)
    {
        hiScoreFlagText.text = Mathf.Round(newScore).ToString();
        transform.position = new Vector3(highScorePos.x, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)).y, 0);
    }
}
