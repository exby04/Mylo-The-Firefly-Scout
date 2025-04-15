using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BotonesDerrota : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Escenas")]
    public SceneAsset escenaNivel;
    public SceneAsset escenaInicio;
#endif

    [SerializeField] private string nombreEscenaNivel;
    [SerializeField] private string nombreEscenaInicio;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (escenaNivel != null)
            nombreEscenaNivel = escenaNivel.name;

        if (escenaInicio != null)
            nombreEscenaInicio = escenaInicio.name;
#endif
    }

    // Vuelve a cargar la escena del nivel
    public void VolverAJugar()
    {
        if (!string.IsNullOrEmpty(nombreEscenaNivel))
        {
            SceneManager.LoadScene(nombreEscenaNivel);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la escena del nivel.");
        }
    }

    // Carga la escena de inicio
    public void SalirAlInicio()
    {
        if (!string.IsNullOrEmpty(nombreEscenaInicio))
        {
            SceneManager.LoadScene(nombreEscenaInicio);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la escena de inicio.");
        }
    }
}
