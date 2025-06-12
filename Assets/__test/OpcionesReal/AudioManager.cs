using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer Reference")]
    public AudioMixer mixer; 

    private void Awake()
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
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        float dB = Mathf.Log10(volume) * 20;
        mixer.SetFloat("MusicVolume", dB);
        Debug.Log($"Set MusicVolume: {volume} → {dB} dB");
    }

    
    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        float dB = Mathf.Log10(volume) * 20;
        mixer.SetFloat("SFXVolume", dB);
        Debug.Log($"Set SFXVolume: {volume} → {dB} dB");
    }

    
    public float GetMusicVolume()
    {
        if (mixer.GetFloat("MusicVolume", out float dB))
        {
            return Mathf.Pow(10f, dB / 20f);
        }
        return 1f;
    }

    public float GetSFXVolume()
    {
        if (mixer.GetFloat("SFXVolume", out float dB))
        {
            return Mathf.Pow(10f, dB / 20f);
        }
        return 1f;
    }
}
