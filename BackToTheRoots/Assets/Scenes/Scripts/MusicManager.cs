using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Eine Referenz auf den MusicManager

    private AudioSource audioSource; // Die AudioSource, die die Musik abspielt

    void Awake()
    {
        // Überprüfe, ob bereits eine Instanz des MusicManager existiert
        if (instance == null)
        {
            // Wenn keine Instanz existiert, setze diese als die aktive Instanz
            instance = this;
            DontDestroyOnLoad(gameObject); // Verhindert, dass das GameObject bei einem Szenenwechsel zerstört wird
        }
        else
        {
            // Wenn bereits eine Instanz existiert, zerstöre dieses GameObject
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Holt sich die AudioSource-Komponente des GameObjects
        audioSource = GetComponent<AudioSource>();

        // Spielt die Musik ab
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Musik anhalten
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    // Musik fortsetzen
    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

    // Musik stoppen
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
