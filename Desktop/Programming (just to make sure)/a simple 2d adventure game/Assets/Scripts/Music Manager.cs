using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; //only one MusicManager will exist.

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private AudioSource musicSource; // The audio source that plays music
    private Coroutine currentFadeCoroutine; // keeps track of the currently running fade

    //AudioClips to be assigned in Inspector
    public AudioClip defaultMusic;
    public AudioClip ArnellMusic;
    public AudioClip gameOverMusic;
    public AudioClip shopMusic;
    public AudioClip NoMusicAdded;

    private void Awake()
    {
        // Kill Every MusicManager.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // There Can Only Be One MusicManager...
        }
        else
        {
            Destroy(gameObject); // Behead MusicManagers
        }
    }

    private AudioClip lastClip;
    private bool isPaused = false;
    private float lastTime; // store where the last clip was paused

    public void PlayPauseMusic(AudioClip pauseClip)
    {
        if (musicSource.clip != null)
            lastClip = musicSource.clip; // Save currently playing clip

        isPaused = true;
        PlayMusic(pauseClip); // Switch to pause theme
    }

    public void ResumePreviousMusic()
    {
        if (isPaused && lastClip != null)
        {
            PlayMusic(lastClip); // Restore old music
            isPaused = false;
        }
    }

    // Instantly switch to pause music (no fade)
    public void PlayPauseMusicInstant(AudioClip pauseClip)
    {
        if (pauseClip == null) return;

        if (musicSource.clip != null)
        {
            lastClip = musicSource.clip; // save currently playing clip
            lastTime = musicSource.time;  // save current playback time
        }

        isPaused = true;
        ForcePlayMusic(pauseClip); // instant switch
    }

    // Instantly resume previous music (no fade), continues from last position
    public void ResumePreviousMusicInstant()
    {
        if (isPaused && lastClip != null)
        {
            isPaused = false;
            musicSource.clip = lastClip;   // restore clip
            musicSource.time = lastTime;    // resume from saved time
            musicSource.Play();             // play from where it left off
        }
    }

    // Call this to play EveryMusic
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return; // do nothing if already playing

        if (!musicSource.isPlaying)
        {
            ForcePlayMusic(clip); // start immediately if nothing is playing
        }
        else
        {
            // Stop any existing fade coroutine before starting a new one
            if (currentFadeCoroutine != null)
                StopCoroutine(currentFadeCoroutine);

            currentFadeCoroutine = StartCoroutine(FadeToNewTrack(clip)); // start new fade
        }
        musicSource.volume = 1f; // ensure full volume
        currentFadeCoroutine = null; // mark fade as finished
    }

    // Immediately stop and switch music without fading
    public void ForcePlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    //Fades out the current music and plays the new track
    private IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        float timer = 0f;
        float fadeDuration = 1f;
        float startVolume = musicSource.volume;

        // Fade Out using unscaled time (ignores pause)
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // <<-- important fix
            musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        // Switch to new clip
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade In using unscaled time (ignores pause)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // <<-- important fix
            musicSource.volume = Mathf.Lerp(0f, startVolume, timer / fadeDuration);
            yield return null;
        }
    }

    // ▶️ Called when the game first starts
    private void Start()
    {
        ForcePlayMusic(defaultMusic); // O, Dullahan...
    }

    // OPTIONAL helper method to play shop music easily
    public void PlayshopMusic()
    {
        PlayMusic(shopMusic); // Ride to Death, Dullahan!
    }
    public AudioSource GetAudioSource()
    {
        return musicSource;
    }
    // Play a clip with custom properties (volume, pitch, pan, priority)
    public void PlayMusicWithSettings(AudioClip clip, float volume = 1f, float pitch = 1f, float panStereo = 0f, int priority = 128)
    {
        if (clip == null) return;

        // Apply audio properties
        musicSource.volume = volume;
        musicSource.pitch = pitch;
        musicSource.panStereo = panStereo;
        musicSource.priority = priority;

        // Use your existing PlayMusic logic to handle fade etc.
        PlayMusic(clip);
    }

}