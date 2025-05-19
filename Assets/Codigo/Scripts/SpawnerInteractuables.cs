using UnityEngine;
using System.Collections.Generic;

public class SpawnerInteractuables : MonoBehaviour
{
    [Header("Prefabs de Cofres")]
    public GameObject cofreNormalPrefab;
    public GameObject cofreTrampaPrefab;

    [Header("Configuración")]
    public int cantidadCofresNormales = 6;
    [Range(0f, 1f)]
    public float probabilidadTrampa = 0.4f;

    void Start()
    {
        // Encuentra todos los puntos marcados con la tag "UbicacionItem"
        GameObject[] puntosDeSpawn = GameObject.FindGameObjectsWithTag("UbicacionItem");

        if (puntosDeSpawn.Length < cantidadCofresNormales)
        {
            Debug.LogError("No hay suficientes puntos para colocar los cofres normales.");
            return;
        }

        // Mezclar las ubicaciones disponibles
        List<GameObject> ubicacionesDisponibles = new List<GameObject>(puntosDeSpawn);
        Shuffle(ubicacionesDisponibles);

        // Instanciar cofres normales
        for (int i = 0; i < cantidadCofresNormales; i++)
        {
            Transform punto = ubicacionesDisponibles[i].transform;
            Instantiate(cofreNormalPrefab, punto.position, punto.rotation);
        }

        // Instanciar cofres trampa aleatorios (en las ubicaciones restantes)
        for (int i = cantidadCofresNormales; i < ubicacionesDisponibles.Count; i++)
        {
            if (Random.value < probabilidadTrampa)
            {
                Transform punto = ubicacionesDisponibles[i].transform;
                Instantiate(cofreTrampaPrefab, punto.position, punto.rotation);
            }
        }
    }

    // Mezclar lista (algoritmo Fisher-Yates)
    void Shuffle(List<GameObject> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);
            GameObject temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }
}
