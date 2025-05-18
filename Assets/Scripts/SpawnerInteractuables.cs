using UnityEngine;

public class SpawnerInteractuables : MonoBehaviour
{
    public GameObject cofreNormalPrefab;
    public GameObject cofreTrampaPrefab;
    public float probabilidadTrampa = 0.5f; // 50% chance de ser trampa

    void Start()
    {
        GameObject[] puntos = GameObject.FindGameObjectsWithTag("UbicacionItem");

        foreach (GameObject punto in puntos)
        {
            GameObject prefabAInstanciar = (Random.value < probabilidadTrampa) ? cofreTrampaPrefab : cofreNormalPrefab;
            Instantiate(prefabAInstanciar, punto.transform.position, Quaternion.identity);
        }
    }
}
