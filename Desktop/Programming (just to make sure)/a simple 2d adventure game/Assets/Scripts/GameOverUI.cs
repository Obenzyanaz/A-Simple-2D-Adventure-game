using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] FadeController fadeController; // Reference to your FadeController
    [SerializeField] Transform spawnPoint;          // Reference to the spawn point in the scene

    public void RestartLevel()
    {
        StartCoroutine(RestartLevelCoroutine());
    }

    IEnumerator RestartLevelCoroutine()
    {
        if (fadeController != null)
        {
            yield return fadeController.FadeOut(1f); // Fade out over 1 second
        }

        // Move player back to the spawn point
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }

        MusicManager.instance.PlayMusic(MusicManager.instance.defaultMusic);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game...");
    }
}
