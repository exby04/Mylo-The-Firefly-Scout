using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Botón 'Volver a jugar' pulsado");
        SceneManager.LoadScene("Level0"); 
    }

    public void ReturnToMenu()
    {
        Debug.Log("Botón 'Salir al inicio' pulsado");
        SceneManager.LoadScene("MainMenu"); // REPLACE MAINMENU WITH REAL NAME
    }
}
