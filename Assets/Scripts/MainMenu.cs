using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string playGameScene;

	public void PlayGame()
    {
        SceneManager.LoadScene(playGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
