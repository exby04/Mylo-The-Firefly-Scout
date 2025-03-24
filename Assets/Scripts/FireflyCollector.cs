using UnityEngine;

public class FireflyCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Luciérnagas recolectadas!");
            gameObject.SetActive(false); // Desaparece el Particle System
            // Falta logica para guardar en el inventario
        }
    }
}
