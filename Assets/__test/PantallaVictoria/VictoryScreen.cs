using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [Header("Scene Settings")]
    public string levelSceneName = "Level0";       // Scene to restart the game
    public string menuSceneName = "MainMenu";      // Scene for returning to main menu

    public void RestartGame()
    {
        Debug.Log(" Botón 'Volver a jugar' pulsado");
        if (!string.IsNullOrEmpty(levelSceneName))
        {
            SceneManager.LoadScene(levelSceneName);
        }
        else
        {
            Debug.LogWarning(" No se ha asignado el nombre de la escena del nivel.");
        }
    }

    public void ReturnToMenu()
    {
        Debug.Log("Botón 'Salir al inicio' pulsado");
        if (!string.IsNullOrEmpty(menuSceneName))
        {
            SceneManager.LoadScene(menuSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el nombre de la escena del menú.");
        }
    }
}
