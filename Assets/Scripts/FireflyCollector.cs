using UnityEngine;

public class FireflyCollector : MonoBehaviour
{
    private bool canCollect = false; // Indica si el jugador está dentro del área

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Presiona 'E' para recoger las luciérnagas.");
            canCollect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = false; 
        }
    }

    private void Update()
    {
        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Luciérnagas recolectadas!");
            gameObject.SetActive(false); // Desaparece
            // Aquí puedes agregar la lógica para guardarlas en el inventario
        }
    }
}
