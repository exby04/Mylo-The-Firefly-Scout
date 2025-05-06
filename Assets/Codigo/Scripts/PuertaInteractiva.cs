using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AbrirPuertasFinal : MonoBehaviour
{
    [Header("Puerta con animación combinada")]
    public Animator animatorPuerta; // El Animator con el controller que tiene la animación
    public string nombreAnimacion = "AbrirPuertas"; // Nombre del state en el Animator Controller

    [Header("Collider que bloquea el paso")]
    public Collider colliderPuerta; // Collider físico que impide el paso, se desactiva al abrir

    [Header("Escena Final")]
#if UNITY_EDITOR
    public SceneAsset escenaVictoria; // Solo visible en el editor
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
                // Ejecutar la animación
                animatorPuerta.Play(nombreAnimacion);

                // Desactivar el collider para dejar pasar
                if (colliderPuerta != null)
                    colliderPuerta.enabled = false;

                // Cargar la escena tras 2 segundos
                Invoke(nameof(FinalizarJuego), 2f);
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
