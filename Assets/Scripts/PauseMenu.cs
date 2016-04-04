using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenuScene;
    private GameManager gameManager;

    public GameObject pauseMenu;

    void Awake ()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PauseGame ()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void RestartGame ()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameManager.resetting = true;
        gameManager.FinishReset();
    }

    public void MainMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
