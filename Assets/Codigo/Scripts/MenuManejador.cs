using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManejador : MonoBehaviour
{
    [Header("Escena que se cargará al pulsar Jugar")]

#if UNITY_EDITOR
    [SerializeField] private SceneAsset escenaEditor;
#endif

    [SerializeField] private string nombreEscena = "Level0";

    public void Jugar()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se ha especificado el nombre de la escena.");
        }
    }

    public void Configuracion()
    {
        Debug.Log("Menú de configuración (placeholder)");
    }

    public void Salir()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Saliendo del juego...");
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (escenaEditor != null)
        {
            nombreEscena = escenaEditor.name;
        }
    }
#endif
}
