using UnityEngine;
using UnityEngine.SceneManagement; // Zum Laden der Szenen
using System.Collections; // Für Coroutines

public class WinTrigger : MonoBehaviour
{
    public string winnerSceneName = "Winner"; // Name der Gewinner-Szene
    public float pauseDuration = 3f; // Dauer der Pause in Sekunden

    [Header("Win-Effekt")]
    public GameObject fxPrefab; // FX GameObject im Inspector zuweisen (z. B. Partikeleffekt)

    private void OnTriggerEnter(Collider other)
    {
        // FX sichtbar machen, falls gesetzt
        if (fxPrefab != null)
        {
            fxPrefab.SetActive(true);
            Debug.Log("FX aktiviert.");
        }
        else
        {
            Debug.LogWarning("FX Prefab ist nicht zugewiesen.");
        }

        // Überprüfen, ob der Spieler den Trigger berührt
        if (other.CompareTag("Player")) // Hier "Player" als Tag des Spielers verwenden
        {
            // Coroutine starten, die für die Pause sorgt und dann zur WinnerScreen-Szene wechselt
            StartCoroutine(WaitAndLoadWinnerScreen());
        }

        //debug log
        Debug.Log("WinTrigger activated by: " + other.name);
    }

    private IEnumerator WaitAndLoadWinnerScreen()
    {
        // Warten für die angegebene Dauer
        yield return new WaitForSecondsRealtime(pauseDuration); // Verwende WaitForSecondsRealtime, um unabhängig von Time.timeScale zu warten

        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadSceneWithFade("Winner");
            Debug.Log("Loading Winner scene with fade effect.");
        }
        else
        {
            // Fallback ohne Fade
            SceneManager.LoadScene("Winner");
            Debug.Log("Fallback: Loading Winner scene without fade.");
        }
    }
}
