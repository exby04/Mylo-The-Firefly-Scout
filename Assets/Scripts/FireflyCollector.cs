using UnityEngine;

public class FireflyCollector : MonoBehaviour
{
    private bool canCollect = false;
    private GameObject player;

    private void Start()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Presiona 'E' para recoger las luciérnagas.");
            canCollect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            canCollect = false;
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

            // gameObject.SetActive(false);
        }
    }
}
