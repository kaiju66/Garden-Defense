using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChoose : MonoBehaviour
{
    public GameObject chooseGame;

    public void StandartGame()
    {
        FindFirstObjectByType<SceneTransition>().LoadScene("StandartGame");
        chooseGame.SetActive(false);
    }

    public void AdvancedGame()
    {
        FindFirstObjectByType<SceneTransition>().LoadScene("AdvancedGame");
        chooseGame.SetActive(false);
    }
}