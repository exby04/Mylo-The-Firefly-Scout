using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BotonesDerrota : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Escenas (solo en Editor)")]
    public SceneAsset escenaNivel;
    public SceneAsset escenaInicio;
#endif

    [Header("Nombres de escena (se usan en la build)")]
    [SerializeField] private string nombreEscenaNivel;
    [SerializeField] private string nombreEscenaInicio;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (escenaNivel != null)
        {
            nombreEscenaNivel = escenaNivel.name;
            Debug.Log($"[BotonesDerrota] Nombre escena nivel seteado: {nombreEscenaNivel}");
        }

        if (escenaInicio != null)
        {
            nombreEscenaInicio = escenaInicio.name;
            Debug.Log($"[BotonesDerrota] Nombre escena inicio seteado: {nombreEscenaInicio}");
        }
    }
#endif

    public void VolverAJugar()
    {
        if (!string.IsNullOrEmpty(nombreEscenaNivel))
        {
            CrossfadeManager.Instance.LoadScene(nombreEscenaNivel);
        }
        else
        {
            Debug.LogWarning("[BotonesDerrota] No se ha asignado la escena del nivel.");
        }
    }

    public void SalirAlInicio()
    {
        if (!string.IsNullOrEmpty(nombreEscenaInicio))
        {

            CrossfadeManager.Instance.LoadScene(nombreEscenaInicio);
        }
        else
        {
            Debug.LogWarning("[BotonesDerrota] No se ha asignado la escena de inicio.");
        }
    }
}
