using UnityEngine;

public class MostrarMensajeInteraccion_powerup : MonoBehaviour
{
    public GameObject mensajeUI;         
    public Transform jugador;           
    public float distanciaDeteccion = 3f;

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
            
            mensajeUI.transform.LookAt(Camera.main.transform);
            mensajeUI.transform.Rotate(0, 180, 0);
        }
    }
}
