using UnityEngine;

public class MostrarMensajeInteraccion : MonoBehaviour
{
    public GameObject mensajeUI;
    public float distanciaDeteccion = 4f;

    private Transform jugador;

    void Start()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
            jugador = player.transform;
    }

    void Update()
    {
        if (jugador == null || mensajeUI == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);
        mensajeUI.SetActive(distancia <= distanciaDeteccion);

        if (mensajeUI.activeSelf)
        {
            mensajeUI.transform.LookAt(Camera.main.transform);
            mensajeUI.transform.Rotate(0, 180, 0);
        }
    }
}

