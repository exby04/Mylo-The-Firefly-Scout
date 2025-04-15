using UnityEngine;

public class MostrarMensajeInteraccion : MonoBehaviour
{
    public GameObject mensajeUI;
    public Transform jugador;
    public float distanciaDeteccion = 4f;

    void Start()
    {
        // Obtener al jugador mediante FindFirstObjectByType
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            jugador = playerController.transform;
        }
    }

    void Update()
    {
        if (jugador == null || mensajeUI == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);
        mensajeUI.SetActive(distancia <= distanciaDeteccion);

        if (mensajeUI.activeSelf)
        {
            // Que mire siempre hacia la cámara
            mensajeUI.transform.LookAt(Camera.main.transform);
            mensajeUI.transform.Rotate(0, 180, 0);
        }
    }
}
