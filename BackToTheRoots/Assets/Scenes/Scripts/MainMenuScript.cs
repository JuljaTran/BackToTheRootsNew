using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;

    private void Start()
    {
        // Buttons-Listener
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.AddListener(QuitGame);

        // Stelle sicher, dass der Cursor sichtbar und nicht gesperrt ist, wenn wir im Menü sind
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadSceneWithFade("MainScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
        // Wenn das Spiel startet, sperre den Cursor und verstecke ihn
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Debug.Log("Spiel beendet!");
        Application.Quit();
    }
}
