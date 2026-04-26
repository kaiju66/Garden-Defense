using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        FindFirstObjectByType<SceneTransition>().LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings clicked");
    }
}