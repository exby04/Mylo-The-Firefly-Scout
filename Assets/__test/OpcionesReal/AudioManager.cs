using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource[] musicSources; // All music tracks
    public AudioSource[] sfxSources;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float volume)
    {
        foreach (var source in musicSources)
        {
            if (source != null)
                source.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var source in sfxSources)
        {
            if (source != null)
                source.volume = volume;
        }
    }
}
