using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirPuertasFinal : MonoBehaviour
{
    public Animator puertaIzquierda;
    public Animator puertaDerecha;
    public string nombreAnimacionIzquierda = "AbrirPuertaIzquierda";
    public string nombreAnimacionDerecha = "AbrirPuertaDerecha";

    public Collider colliderIzquierda;  // Asigna el collider de la puerta izquierda
    public Collider colliderDerecha;    // Asigna el collider de la puerta derecha

    private bool jugadorDentro = false;

    private void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (Inventario.instance.TieneLlave())
            {
                // Ejecutar animaciones
                puertaIzquierda.Play(nombreAnimacionIzquierda);
                puertaDerecha.Play(nombreAnimacionDerecha);

                // Desactivar colliders para permitir el paso
                if (colliderIzquierda != null) colliderIzquierda.enabled = false;
                if (colliderDerecha != null) colliderDerecha.enabled = false;

                // Fin del juego o siguiente escena (opcional)
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
        // SceneManager.LoadScene("PantallaFinal"); // si tienes otra escena
        // Application.Quit(); // si es build
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
