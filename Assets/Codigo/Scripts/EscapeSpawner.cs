using UnityEngine;

public class EscapeSpawner : MonoBehaviour
{
    [Header("Prefab de la Puerta de Escape")]
    public GameObject puertaEscapePrefab;

    void Start()
    {
        // Buscar puntos marcados como posibles ubicaciones de escape
        GameObject[] puntosDeEscape = GameObject.FindGameObjectsWithTag("UbicacionEscape");

        if (puntosDeEscape.Length == 0)
        {
            Debug.LogError("No se encontraron ubicaciones con la tag 'UbicacionEscape'.");
            return;
        }

        // Elegir uno al azar
        GameObject puntoElegido = puntosDeEscape[Random.Range(0, puntosDeEscape.Length)];
        Transform punto = puntoElegido.transform;

        // Instanciar la puerta con la posición y rotación del punto
        Instantiate(puertaEscapePrefab, punto.position, punto.rotation);
    }
}
