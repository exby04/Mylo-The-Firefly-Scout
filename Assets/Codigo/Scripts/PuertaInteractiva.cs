using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AbrirPuertasFinal : MonoBehaviour, IInteractable
{
    [Header("Puerta con animaci�n combinada")]
    public Animator animatorPuerta;
    public string nombreAnimacion = "AbrirPuertas";

    [Header("Collider que bloquea el paso")]
    public Collider colliderPuerta;

    [Header("Escena Final")]
#if UNITY_EDITOR
    public SceneAsset escenaVictoria;
#endif
    [SerializeField] private string nombreEscenaVictoria;

    [Header("Interacci�n")]
    public float tiempoParaAbrir = 2f;

    private bool yaSeAbrio = false;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (escenaVictoria != null)
            nombreEscenaVictoria = escenaVictoria.name;
#endif
    }

    
    public float HoldDuration => tiempoParaAbrir;

    public void OnInteract()
    {
        if (yaSeAbrio) return;

        if (Inventario.instance.TieneLlave())
        {
            yaSeAbrio = true;

            
            if (animatorPuerta != null)
                animatorPuerta.Play(nombreAnimacion);

            
            if (colliderPuerta != null)
                colliderPuerta.enabled = false;

            
            Invoke(nameof(FinalizarJuego), 2f);
        }
        else
        {
            Debug.Log("Necesitas una llave para abrir las puertas.");
        }
    }

    private void FinalizarJuego()
    {
        Debug.Log("�Juego finalizado!");

        if (!string.IsNullOrEmpty(nombreEscenaVictoria))
        {
            CrossfadeManager.Instance.FadeThroughScenes("VictoryTransitionScene", 5f, "VictoryScene");
        }
        else
        {
            Debug.LogWarning("No se asign� la escena de victoria.");
        }
    }
}
