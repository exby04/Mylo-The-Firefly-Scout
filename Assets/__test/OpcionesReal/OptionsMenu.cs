using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle generalToggle;
    [SerializeField] private Slider generalSlider;

    [Header("Panel Control")]
    [SerializeField] private GameObject panelOpciones;
    [SerializeField] private GameObject pauseMenuUI;
    public bool isInGame = false;

    private float lastMusicVolume = 1f;
    private float lastSFXVolume = 1f;
    private float lastGeneralVolume = 1f;

    private bool inicializado = false;

    void Awake()
    {
        if (!musicToggle) Debug.LogError("musicToggle no asignado.");
        if (!musicSlider) Debug.LogError("musicSlider no asignado.");
        if (!sfxToggle) Debug.LogError("sfxToggle no asignado.");
        if (!sfxSlider) Debug.LogError("sfxSlider no asignado.");
        if (!fullscreenToggle) Debug.LogError("fullscreenToggle no asignado.");
        if (!generalToggle) Debug.LogError("generalToggle no asignado.");
        if (!generalSlider) Debug.LogError("generalSlider no asignado.");
        if (!panelOpciones) Debug.LogError("panelOpciones no asignado.");
        if (!pauseMenuUI) Debug.LogError("pauseMenuUI no asignado.");
    }

    void Start()
    {
        inicializado = false;

        if (generalSlider != null)
            generalSlider.value = PlayerPrefs.GetFloat("GeneralVolume", 1f);
        if (generalToggle != null)
            generalToggle.isOn = PlayerPrefs.GetInt("GeneralOn", 1) == 1;

        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (musicToggle != null)
            musicToggle.isOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        if (sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        if (sfxToggle != null)
            sfxToggle.isOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;

        inicializado = true;

        ApplyAudioSettings();
    }

    public void AbrirOpciones()
    {
        isInGame = true;
        if (panelOpciones != null)
            panelOpciones.SetActive(true);
        Time.timeScale = 0f;

        Debug.Log("Panel de opciones abierto desde el menú de pausa.");
    }

    public void OnMusicVolumeChanged(float value)
    {
        if (!inicializado) return;

        PlayerPrefs.SetFloat("MusicVolume", value);
        if (musicToggle != null)
            musicToggle.isOn = value > 0.001f;
        ApplyAudioSettings();
    }

    public void OnSFXVolumeChanged(float value)
    {
        if (!inicializado) return;

        PlayerPrefs.SetFloat("SFXVolume", value);
        if (sfxToggle != null)
            sfxToggle.isOn = value > 0.001f;
        ApplyAudioSettings();
    }

    public void OnMusicToggleChanged(bool isOn)
    {
        if (!inicializado) return;

        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);

        if (musicSlider != null)
        {
            if (!isOn)
            {
                lastMusicVolume = musicSlider.value > 0 ? musicSlider.value : 0.5f;
                musicSlider.value = 0f;
            }
            else
            {
                musicSlider.value = lastMusicVolume;
            }
        }

        ApplyAudioSettings();
    }

    public void OnSFXToggleChanged(bool isOn)
    {
        if (!inicializado) return;

        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);

        if (sfxSlider != null)
        {
            if (!isOn)
            {
                lastSFXVolume = sfxSlider.value > 0 ? sfxSlider.value : 0.5f;
                sfxSlider.value = 0f;
            }
            else
            {
                sfxSlider.value = lastSFXVolume;
            }
        }

        ApplyAudioSettings();
    }

    public void OnFullscreenToggleChanged(bool isFullscreen)
    {
        if (!inicializado) return;

        Screen.fullScreen = isFullscreen;
    }

    public void OnGeneralVolumeChanged(float value)
    {
        if (!inicializado) return;

        PlayerPrefs.SetFloat("GeneralVolume", value);
        if (generalToggle != null)
            generalToggle.isOn = value > 0.001f;
        ApplyAudioSettings();
    }

    public void OnGeneralToggleChanged(bool isOn)
    {
        if (!inicializado) return;

        PlayerPrefs.SetInt("GeneralOn", isOn ? 1 : 0);

        if (generalSlider != null)
        {
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

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(musicOn ? musicVolume : 0f);
            AudioManager.Instance.SetSFXVolume(sfxOn ? sfxVolume : 0f);
            AudioManager.Instance.SetGeneralVolume(generalOn ? generalVolume : 0f);
        }
        else
        {
            Debug.LogWarning("AudioManager.Instance no está presente en la escena.");
        }
    }

    public void CerrarOpciones()
    {
        Debug.Log("CerrarOpciones fue llamado");

        if (panelOpciones == null)
        {
            Debug.LogError("panelOpciones es null");
            return;
        }

        panelOpciones.SetActive(false);
        Debug.Log("Panel Opciones desactivado");

        Time.timeScale = 1f;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Debug.Log("Menu de pausa activado");
        }
        else
        {
            Debug.LogWarning("pauseMenuUI es null");
        }
    }

}
