using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public string mainMenuScene;
    private GameManager gameManager;

    void Awake ()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void RestartGame ()
    {
        gameManager.FinishReset();
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
