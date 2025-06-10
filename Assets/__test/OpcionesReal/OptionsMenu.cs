using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Toggle musicToggle;
    public Slider musicSlider;
    public Toggle sfxToggle;
    public Slider sfxSlider;
    public Toggle fullscreenToggle;

    [Header("Panel Control")]
    public GameObject panelOpciones;
    public bool isInGame = false; // Set this to true when opened from Pause Menu

    void Start()
    {
        // Load saved settings
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
        musicToggle.isOn = musicOn;
        sfxToggle.isOn = sfxOn;
        fullscreenToggle.isOn = Screen.fullScreen;

        ApplyAudioSettings();
    }

    public void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        ApplyAudioSettings();
    }

    public void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        ApplyAudioSettings();
    }

    public void OnMusicToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
        ApplyAudioSettings();
    }

    public void OnSFXToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
        ApplyAudioSettings();
    }

    public void OnFullscreenToggleChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    void ApplyAudioSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        AudioManager.Instance.SetMusicVolume(musicOn ? musicVolume : 0f);
        AudioManager.Instance.SetSFXVolume(sfxOn ? sfxVolume : 0f);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
        PlayerPrefs.Save();

        if (isInGame)
        {
            Time.timeScale = 1f;
            Debug.Log("Panel de opciones cerrado y juego reanudado.");
        }
        else
        {
            Debug.Log("Panel de opciones cerrado desde el menú de inicio.");
        }
    }
}
