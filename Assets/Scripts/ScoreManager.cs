using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text hiScoreText;

    public float score;
    public float hiScore;

    public float pointsPerSecond;

    public bool scoreIncreasing = true;

    private PlayerController player;
    public Vector3 highScorePos;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        hiScore = PlayerPrefs.GetFloat("HighScore", 0);
        float highScorePosX = PlayerPrefs.GetFloat("HighScorePosX", player.transform.position.x);
        float highScorePosY = PlayerPrefs.GetFloat("HighScorePosY", player.transform.position.y);
        highScorePos = new Vector3(highScorePosX, highScorePosY, 0);
    }
	
	// Update is called once per frame
	void Update () {

        if (scoreIncreasing)
        {
            score += pointsPerSecond * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(2))
        {
            hiScore = 0;
        }

        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetFloat("HighScore", hiScore);
            highScorePos = player.transform.position;
            PlayerPrefs.SetFloat("HighScorePosX", player.transform.position.x);
            PlayerPrefs.SetFloat("HighScorePosY", player.transform.position.y);
        }

        scoreText.text = "Score: " + Mathf.Round(score);
        hiScoreText.text = "High Score: " + Mathf.Round(hiScore);
	}

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
