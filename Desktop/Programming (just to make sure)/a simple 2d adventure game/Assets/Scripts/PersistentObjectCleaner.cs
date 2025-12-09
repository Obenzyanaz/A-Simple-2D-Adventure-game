using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCleanup : MonoBehaviour
{
    void Awake()
    {
        // Only clean up when we're in the Main Menu scene
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Destroy Player
            var player = GameObject.FindGameObjectWithTag("player");
            if (player != null)
                Destroy(player);

            // Destroy MusicManager
            var musicManager = FindObjectOfType<MusicManager>();
            if (musicManager != null)
                Destroy(musicManager.gameObject);

            // Destroy PauseMenu if you made it persistent
            var pauseMenu = FindObjectOfType<PauseMenu>();
            if (pauseMenu != null)
                Destroy(pauseMenu.gameObject);
        }
    }
}