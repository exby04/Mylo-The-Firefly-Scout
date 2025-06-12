using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AbrirPuertasFinal : MonoBehaviour, IInteractable
{
    [Header("Puerta con animación combinada")]
    public Animator animatorPuerta;
    public string nombreAnimacion = "AbrirPuertas";

    [Header("Collider que bloquea el paso")]
    public Collider colliderPuerta;

    [Header("Escena Final")]
#if UNITY_EDITOR
    public SceneAsset escenaVictoria;
#endif
    [SerializeField] private string nombreEscenaVictoria;

    [Header("Interacción")]
    public float tiempoParaAbrir = 2f;

    [Header("Sonidos intento fallido")]
    public AudioClip[] sonidosPuertaBloqueada;

    private AudioSource audioSource;
    private bool yaSeAbrio = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

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

            // Ejecutar la animación
            if (animatorPuerta != null)
                animatorPuerta.Play(nombreAnimacion);

            // Desactivar el collider
            if (colliderPuerta != null)
                colliderPuerta.enabled = false;

            // Cargar la escena tras un retraso
            Invoke(nameof(FinalizarJuego), 0.5f);
        }
        else
        {
            // Sonido aleatorio de intento fallido
            if (sonidosPuertaBloqueada != null && sonidosPuertaBloqueada.Length > 0)
            {
                int index = Random.Range(0, sonidosPuertaBloqueada.Length);
                audioSource.PlayOneShot(sonidosPuertaBloqueada[index]);
            }

            Debug.Log("Necesitas una llave para abrir las puertas.");
        }
    }

    private void FinalizarJuego()
    {
        Debug.Log("¡Juego finalizado!");

        if (!string.IsNullOrEmpty(nombreEscenaVictoria))
        {
            CrossfadeManager.Instance.FadeThroughScenes("VictoryDoorScene", 9f, "VictoryScene");
        }
        else
        {
            Debug.LogWarning("No se asignó la escena de victoria.");
        }
    }
}
