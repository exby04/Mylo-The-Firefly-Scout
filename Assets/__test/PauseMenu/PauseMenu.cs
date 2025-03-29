using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;      // The pause menu panel (with all buttons)
    public GameObject exitWarningUI;    // The exit confirmation popup

    private bool isPaused = false;

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
        exitWarningUI.SetActive(false); // In case it was open
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
        Debug.Log("Salir al Inicio clicked — waiting for main menu to be implemented.");
        // Later: SceneManager.LoadScene("MainMenu");
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
        Debug.Log("Game closed.");
        Application.Quit();
    }
}
