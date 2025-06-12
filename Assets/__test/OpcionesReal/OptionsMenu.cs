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
    public Toggle generalToggle;
    public Slider generalSlider;


    [Header("Panel Control")]
    public GameObject panelOpciones;
    public bool isInGame = false; 
    private float lastMusicVolume = 1f;
    private float lastSFXVolume = 1f;
    private float lastGeneralVolume = 1f;



    void Start()
    {
        float generalVol = PlayerPrefs.GetFloat("GeneralVolume", 1f);
        bool generalOn = PlayerPrefs.GetInt("GeneralOn", 1) == 1;
        generalSlider.value = generalVol;
        generalToggle.isOn = generalOn;

        
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
    public void AbrirOpciones()
    {
     isInGame = true; 
     panelOpciones.SetActive(true);
     Time.timeScale = 0f;

     Debug.Log("Panel de opciones abierto desde el menú de pausa.");
    }

    public void OnMusicVolumeChanged(float value)
    {
     PlayerPrefs.SetFloat("MusicVolume", value);
     musicToggle.isOn = value > 0.001f; 
     ApplyAudioSettings();
    }

    
    public void OnSFXVolumeChanged(float value)
    {
    PlayerPrefs.SetFloat("SFXVolume", value);
    sfxToggle.isOn = value > 0.001f;
    ApplyAudioSettings();
    }

   public void OnMusicToggleChanged(bool isOn)
    {
    PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);

    if (!isOn)
    {
        lastMusicVolume = musicSlider.value > 0 ? musicSlider.value : 0.5f;
        musicSlider.value = 0f;
    }
    else
    {
        musicSlider.value = lastMusicVolume;
    }

    ApplyAudioSettings();
   }

    public void OnSFXToggleChanged(bool isOn)
    {
    PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);

    if (!isOn)
    {
        lastSFXVolume = sfxSlider.value > 0 ? sfxSlider.value : 0.5f;
        sfxSlider.value = 0f;
    }
    else
    {
        sfxSlider.value = lastSFXVolume;
    }

    ApplyAudioSettings();
    }


    public void OnFullscreenToggleChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void OnGeneralVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("GeneralVolume", value);
        generalToggle.isOn = value > 0.001f;
        ApplyAudioSettings();
    }

    public void OnGeneralToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("GeneralOn", isOn ? 1 : 0);

        if (!isOn)
        {
            
            if (generalSlider.value > 0.001f)
                lastGeneralVolume = generalSlider.value;

            generalSlider.value = 0f;
        }
        else
        {
            generalSlider.value = lastGeneralVolume > 0.001f ? lastGeneralVolume : 0.5f;
        }

        ApplyAudioSettings();
    }



    void ApplyAudioSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        float generalVolume = PlayerPrefs.GetFloat("GeneralVolume", 1f);

        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        bool generalOn = PlayerPrefs.GetInt("GeneralOn", 1) == 1;

        AudioManager.Instance.SetMusicVolume(musicOn ? musicVolume : 0f);
        AudioManager.Instance.SetSFXVolume(sfxOn ? sfxVolume : 0f);
        AudioManager.Instance.SetGeneralVolume(generalOn ? generalVolume : 0f);
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
