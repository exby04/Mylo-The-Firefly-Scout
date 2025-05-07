using UnityEngine;

public class FireflyCollector : MonoBehaviour, IInteractable
{
    public float tiempoParaRecolectar = 1.5f; // Tiempo para mantener pulsado

    private GameObject player;

    private void Start()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
        }
    }

    // Interfaz IInteractable
    public float HoldDuration => tiempoParaRecolectar;

    public void OnInteract()
    {
        Debug.Log("¡Luciérnagas recolectadas!");

        if (player != null)
        {
            AtenuacionLuz atenuacion = player.GetComponentInChildren<AtenuacionLuz>();
            if (atenuacion != null)
            {
                atenuacion.ReiniciarLuz();
            }
        }

        // Puedes desactivarlo o dejarlo visible
        // gameObject.SetActive(false);
    }
}
