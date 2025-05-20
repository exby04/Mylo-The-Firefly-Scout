using UnityEngine;

public class EscapeSpawner : MonoBehaviour
{
    [Header("Prefab de la Puerta de Escape")]
    public GameObject puertaEscapePrefab;

    void Start()
    {
        GameObject[] puntosDeEscape = GameObject.FindGameObjectsWithTag("UbicacionEscape");

        if (puntosDeEscape.Length == 0)
        {
            Debug.LogError("No se encontraron ubicaciones con la tag 'UbicacionEscape'.");
            return;
        }

        GameObject puntoElegido = puntosDeEscape[Random.Range(0, puntosDeEscape.Length)];
        Transform punto = puntoElegido.transform;

        Instantiate(puertaEscapePrefab, punto.position, punto.rotation);
    }
}
