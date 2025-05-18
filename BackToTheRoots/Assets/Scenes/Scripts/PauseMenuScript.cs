using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start()
    {
        // Menü beim Start sicher deaktivieren
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        Time.timeScale = 1f; // Spiel läuft weiter
        isPaused = false;
    }

    void Pause()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        Time.timeScale = 0f; // Spiel pausieren
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Zeit zurücksetzen bevor Szenewechsel
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadSceneWithFade("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}