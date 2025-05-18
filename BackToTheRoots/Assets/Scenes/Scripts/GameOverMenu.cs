using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Button tryAgainButton;
    public Button mainMenuButton;

    private void Start()
    {
        // Buttons mit Funktionen verbinden
        tryAgainButton.onClick.AddListener(RetryGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        // Mauszeiger sichtbar machen
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RetryGame()
    {
        Debug.Log("Try Again gedr�ckt");

        Time.timeScale = 1f;

        if (SceneTransitionManager.Instance != null)
        {
            Debug.Log("F�hre Fade aus und lade 'MainScene'");
            SceneTransitionManager.Instance.LoadSceneWithFade("MainScene");
        }
        else
        {
            Debug.Log("F�hre Szene ohne Fade aus.");
            SceneManager.LoadScene("MainScene");
        }
    }


    public void ReturnToMainMenu()
    {
        Debug.Log("Zur�ck zum Hauptmen� gedr�ckt");
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadSceneWithFade("MainMenu");
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
