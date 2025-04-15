using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AbrirPuertasFinal : MonoBehaviour
{
    [Header("Puertas")]
    public Animator puertaIzquierda;
    public Animator puertaDerecha;
    public string nombreAnimacionIzquierda = "AbrirPuertaIzquierda";
    public string nombreAnimacionDerecha = "AbrirPuertaDerecha";

    public Collider colliderIzquierda;
    public Collider colliderDerecha;

    [Header("Escena Final")]
#if UNITY_EDITOR
    public SceneAsset escenaVictoria;  // arrastra aquí la escena final
#endif
    [SerializeField] private string nombreEscenaVictoria;

    private bool jugadorDentro = false;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (escenaVictoria != null)
            nombreEscenaVictoria = escenaVictoria.name;
#endif
    }

    private void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (Inventario.instance.TieneLlave())
            {
                // Animaciones
                puertaIzquierda.Play(nombreAnimacionIzquierda);
                puertaDerecha.Play(nombreAnimacionDerecha);

                // Abrir puertas (colliders off)
                if (colliderIzquierda != null) colliderIzquierda.enabled = false;
                if (colliderDerecha != null) colliderDerecha.enabled = false;

                // Esperar 2 segundos y luego ir a la escena final
                Invoke("FinalizarJuego", 2f);
            }
            else
            {
                Debug.Log("Necesitas una llave para abrir las puertas.");
            }
        }
    }

    private void FinalizarJuego()
    {
        Debug.Log("¡Juego finalizado!");

        if (!string.IsNullOrEmpty(nombreEscenaVictoria))
        {
            SceneManager.LoadScene(nombreEscenaVictoria);
        }
        else
        {
            Debug.LogWarning("No se asignó la escena de victoria.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            jugadorDentro = false;
        }
    }
}
