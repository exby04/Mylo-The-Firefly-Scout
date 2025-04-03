using UnityEngine;

public class FireflyCollector : MonoBehaviour
{
    private bool canCollect = false;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Presiona 'E' para recoger las luciérnagas.");
            canCollect = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = false;
            player = null;
        }
    }

    private void Update()
    {
        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Luciérnagas recolectadas!");
            

            AtenuacionLuz atenuacion = player.GetComponentInChildren<AtenuacionLuz>();
            if (atenuacion != null)
            {
                atenuacion.ReiniciarLuz();
            }

            gameObject.SetActive(false);
        }
    }
}
