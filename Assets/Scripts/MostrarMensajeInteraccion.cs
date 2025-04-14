using UnityEngine;

public class MostrarMensajeInteraccion : MonoBehaviour
{
    public GameObject mensajeUI;        
    public Transform jugador;           
    public float distanciaDeteccion = 4f;

    void Update()
    {
        if (jugador == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null) jugador = obj.transform;
        }

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
