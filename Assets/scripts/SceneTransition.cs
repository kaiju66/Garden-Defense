using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public RectTransform panel;
    public float waitTime = 1.2f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        // ⬅ Плавно заїжджає
        yield return StartCoroutine(SlideIn());

        // ⏳ Пауза
        yield return new WaitForSecondsRealtime(waitTime);

        // 🔄 Зміна сцени
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        // ставимо панель в центр перед виїздом
        panel.anchoredPosition = Vector2.zero;

        StartCoroutine(SlideOut());
    }

    IEnumerator SlideIn()
    {
        float width = panel.rect.width;

        Vector2 start = new Vector2(-width, 0);
        Vector2 end = Vector2.zero;

        float time = 0;
        float duration = 0.5f;

        while (time < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(start, end, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        panel.anchoredPosition = end;
    }

    IEnumerator SlideOut()
    {
        float width = panel.rect.width;

        Vector2 start = panel.anchoredPosition;
        Vector2 end = new Vector2(width, 0);

        float time = 0;
        float duration = 0.5f;

        while (time < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(start, end, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        panel.anchoredPosition = end;
    }
}