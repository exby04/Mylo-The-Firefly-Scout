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
            Debug.Log("Presiona 'E' para recoger las luci�rnagas.");
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

        PowerUpLuz powerUp = player.GetComponent<PowerUpLuz>();
        AtenuacionLuz atenuacion = player.GetComponentInChildren<AtenuacionLuz>();

        // Only reset light if power-up is NOT active
        if (powerUp != null && powerUp.EstaActivo)
        {
            Debug.Log("No se reinicia la luz porque el power-up de visión está activo.");
            return;
        }

        if (atenuacion != null)
        {
            atenuacion.ReiniciarLuz();
        }

        // gameObject.SetActive(false);
    }
}

}
