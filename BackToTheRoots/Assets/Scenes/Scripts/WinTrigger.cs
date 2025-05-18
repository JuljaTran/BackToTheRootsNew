using UnityEngine;
using UnityEngine.SceneManagement; // Zum Laden der Szenen
using System.Collections; // Für Coroutines

public class WinTrigger : MonoBehaviour
{
    public string winnerSceneName = "WinnerScreen"; // Name der Gewinner-Szene
    public float pauseDuration = 3f; // Dauer der Pause in Sekunden

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob der Spieler den Trigger berührt
        if (other.CompareTag("Player")) // Hier "Player" als Tag des Spielers verwenden
        {
            // Coroutine starten, die für die Pause sorgt und dann zur WinnerScreen-Szene wechselt
            StartCoroutine(WaitAndLoadWinnerScreen());
        }
    }

    private IEnumerator WaitAndLoadWinnerScreen()
    {
        // Warten für die angegebene Dauer
        yield return new WaitForSecondsRealtime(pauseDuration); // Verwende WaitForSecondsRealtime, um unabhängig von Time.timeScale zu warten

        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadSceneWithFade("WinnerScreen");
        }
        else
        {
            // Fallback ohne Fade
            SceneManager.LoadScene("WinnerScreen");
        }
    }
}
