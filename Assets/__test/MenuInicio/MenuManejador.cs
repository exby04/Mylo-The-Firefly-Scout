using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManejador : MonoBehaviour
{
    [Header("Escena a cargar al pulsar Jugar")]
    [SerializeField] private UnityEngine.Object escenaJuego;

    public void Jugar()
    {
        if (escenaJuego != null)
        {
            string escenaNombre = escenaJuego.name;
            SceneManager.LoadScene(escenaNombre);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No se ha asignado ninguna escena al botón Jugar.");
        }
    }

    public void Configuracion()
    {
        UnityEngine.Debug.Log("Abriendo menú de configuración...");
    }
    //Si se hara en webgl habra que ver como cerrarlo 
    public void Salir()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        UnityEngine.Debug.Log("Saliendo del juego...");
    }
}
