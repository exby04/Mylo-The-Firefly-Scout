using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;
    public GameObject exitWarningUI;

    [Header("Scene Settings")]
#if UNITY_EDITOR
    public SceneAsset menuSceneAsset; // Solo visible en el editor
#endif
    public string menuSceneName; 

    private bool isPaused = false;

    void Awake()
    {
#if UNITY_EDITOR
        if (menuSceneAsset != null)
        {
            menuSceneName = menuSceneAsset.name; // Solo lo hace en el editor
            Debug.Log("Asignado automáticamente el nombre de la escena: " + menuSceneName);
        }
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseMenuUI.SetActive(false);
        exitWarningUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        Debug.Log("Pause() called");
        pauseMenuUI.SetActive(true);
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
}
