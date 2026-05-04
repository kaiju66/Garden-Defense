using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingP;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // меню спочатку вимкнене
        Time.timeScale = 1f; // гра йде нормально
    }

    public void ToggleMenu()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // ⏸️ ЗУПИНЯЄМО ЧАС
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingP.SetActive(false);
        Time.timeScale = 1f; // ▶️ ПРОДОВЖУЄМО ГРУ
        isPaused = false;
        
    }

    public void Restart()
    {
        Time.timeScale = 1f; // обов’язково!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // обов’язково!
        FindFirstObjectByType<SceneTransition>().LoadScene("menuGame");
        
    }

    public void OpenSettings()
    {
      settingP.SetActive(!settingP.activeSelf);
    }

    public void CloseP()
    {
        if (settingP.activeSelf)
        {
            settingP.SetActive(false);
        }
    }
}