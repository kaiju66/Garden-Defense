using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject chooseGame;
    public GameObject settingP;

    public void StartGame()
    {
        chooseGame.SetActive(true);
        settingP.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void OpenSettings()
    {
       settingP.SetActive(!settingP.activeSelf);
    }
}