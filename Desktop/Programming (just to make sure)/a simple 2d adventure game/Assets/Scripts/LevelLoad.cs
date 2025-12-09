using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider slider;
    public FadeController fadeController;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadWithFade(sceneIndex));
    }

    IEnumerator LoadWithFade(int sceneIndex)
    {
        if (fadeController != null)

            yield return StartCoroutine(fadeController.FadeOut(1f));
        LoadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
        yield return StartCoroutine(fadeController.FadeIn(1f));
    }
}