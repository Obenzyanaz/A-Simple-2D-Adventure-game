using UnityEngine;
using System.Collections; // Needed for IEnumerator & coroutines

public class SceneMusicSetter : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioClip sceneMusic;        // Music for this scene
    [SerializeField] private float delayBeforePlay = 0f;  // Delay before starting (seconds)

    [Header("Audio Properties")]
    [Range(0f, 1f)]
    [SerializeField] private float sceneVolume = 1f;      // Loudness (0 = mute, 1 = full)
    [Range(-3f, 3f)]
    [SerializeField] private float scenePitch = 1f;       // Playback speed/pitch (1 = normal)
    [Range(-1f, 1f)]
    [SerializeField] private float sceneStereoPan = 0f;   // -1 = left, 0 = center, 1 = right
    [Range(0, 256)]
    [SerializeField] private int scenePriority = 128;     // Lower = higher priority

    private void Start()
    {
        if (sceneMusic != null && MusicManager.instance != null)
        {
            StartCoroutine(PlayMusicWithDelay());
        }
    }

    private IEnumerator PlayMusicWithDelay()
    {
        if (delayBeforePlay > 0f)
            yield return new WaitForSeconds(delayBeforePlay);

        if (MusicManager.instance != null && sceneMusic != null)
        {
            // Tell MusicManager to play this clip with your scene settings
            MusicManager.instance.PlayMusicWithSettings(
                sceneMusic,
                sceneVolume,
                scenePitch,
                sceneStereoPan,
                scenePriority
            );
        }
    }
}