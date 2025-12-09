using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Singleton instance
    public static PauseMenu instance;
    public static bool GameIsPaused = false;

    [Header("UI & Audio")]
    public GameObject pauseMenuUI;
    public AudioClip pauseMusic;

    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep alive across scenes
        }
        else
        {
            Destroy(gameObject); // Remove duplicate pause menus
        }
    }

    private void Start()
    {
        // Hide pause menu at the start
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        // Toggle pause on Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        GameIsPaused = true;

        // Show pause menu
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        // Stop game time
        Time.timeScale = 0f;

        // Switch to pause music instantly
        if (MusicManager.instance != null && pauseMusic != null)
            MusicManager.instance.PlayPauseMusicInstant(pauseMusic);
    }

    public void Resume()
    {
        GameIsPaused = false;

        // Hide pause menu
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        // Resume game time
        Time.timeScale = 1f;

        // Resume previous music instantly
        if (MusicManager.instance != null)
            MusicManager.instance.ResumePreviousMusicInstant();
    }

    public void LoadMenu()
    {
        // Make sure time is normal before loading menu
        Time.timeScale = 1f;

        // Load main menu scene
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Выходим...");
        Application.Quit();
    }
}