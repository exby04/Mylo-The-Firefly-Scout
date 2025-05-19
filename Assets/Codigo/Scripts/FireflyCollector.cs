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
        if (player == null) return;

        PowerUpLuz powerUp = player.GetComponent<PowerUpLuz>();
        AtenuacionLuz atenuacion = player.GetComponentInChildren<AtenuacionLuz>();

        // Solo reinicia la luz si el power-up no está activo
        if (powerUp != null && powerUp.EstaActivo)
        {
            Debug.Log("No se reinicia la luz porque el power-up de visión está activo.");
            return;
        }

        if (atenuacion != null)
        {
            atenuacion.ReiniciarLuz();
        }

        Debug.Log("¡Luciérnagas recolectadas!");
        // gameObject.SetActive(false);
    }
}
