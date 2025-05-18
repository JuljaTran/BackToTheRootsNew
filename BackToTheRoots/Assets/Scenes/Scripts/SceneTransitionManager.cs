using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 2f;
    private bool isFading = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Damit der SceneTransitionManager über Szenenwechsel hinweg erhalten bleibt
        }
        else
        {
            Destroy(gameObject); // Zerstöre zusätzliche Instanzen
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f; // Sicherstellen, dass Spiel nach jeder Szene läuft

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
            StartCoroutine(FadeIn());
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScene(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        isFading = true;
        yield return StartCoroutine(FadeOut());

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
        isFading = false;
    }
}
