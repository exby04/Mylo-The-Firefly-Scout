using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;      // MUST BE ASSIGNED
    public GameObject exitWarningUI;    // Optional, unless used

    [Header("Scene Settings")]
#if UNITY_EDITOR
    public SceneAsset menuSceneAsset;   
    public SceneAsset restartSceneAsset;
#endif
    public string menuSceneName;        
    public string restartSceneName; 

    private bool isPaused = false;

    void Awake()
    {
#if UNITY_EDITOR
        if (menuSceneAsset != null)
        {
            menuSceneName = menuSceneAsset.name;
            Debug.Log("Asignado automáticamente el nombre de la escena: " + menuSceneName);
        }

        if (restartSceneAsset != null)
    {
        restartSceneName = restartSceneAsset.name;
        Debug.Log("Asignado automáticamente el nombre de la escena de reinicio: " + restartSceneName);
    }

#endif
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // GOOD: Triggers on Esc
        {
            Debug.Log("ESC key pressed");
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("Resume() called");
        pauseMenuUI.SetActive(false);   // Hides menu
        exitWarningUI.SetActive(false); // Hides warning (if any)
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        Debug.Log("Pause() called");

        if (pauseMenuUI == null)
        {
            Debug.LogError("pauseMenuUI is NOT assigned!");
            return;
        }

        pauseMenuUI.SetActive(true);    // <--- This is where it should show
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenOptions()
    {
        Debug.Log("Opciones button clicked — functionality not implemented yet.");
    }

    public void ExitToMenu()
    {
        Debug.Log("Salir al Inicio clicked");
        Time.timeScale = 1f;

        Debug.Log("Intentando cargar escena: " + menuSceneName);

        if (!string.IsNullOrEmpty(menuSceneName))
        {
            SceneManager.LoadScene(menuSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la escena del menú en el Inspector.");
        }
    }

    public void ExitGamePrompt()
    {
        Debug.Log("ExitGamePrompt() called — showing exit confirmation popup.");
        exitWarningUI.SetActive(true);
    }

    public void CancelExit()
    {
        Debug.Log("CancelExit() called — hiding confirmation popup.");
        exitWarningUI.SetActive(false);
    }

    public void ConfirmExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Saliendo del juego...");
    }

    public void RestartLevel()
    {
        Debug.Log("RestartLevel() called");
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(restartSceneName))
        {
            SceneManager.LoadScene(restartSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la escena de reinicio en el Inspector.");
        }
    }


}
