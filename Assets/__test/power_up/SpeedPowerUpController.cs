using UnityEngine;

public class SpeedPowerUpController : MonoBehaviour
{
    public PlayerController playerController;
    public float speedMultiplier = 2f;
    public float powerUpDuration = 10f;

    private bool hasPowerUp = false;       // Recibido desde el inventario
    private bool isActive = false;         // Efecto activado
    private float timer = 0f;
    private float originalSpeed;

    void Start()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        originalSpeed = playerController.playerSpeed;
        

    }

    void Update()
    {
        // Activar si tiene el powerUp y pulsa espacio
        if (hasPowerUp && !isActive && Input.GetKeyDown(KeyCode.Space))
        {
            ActivatePowerUp();
        }

        // Temporizador activo
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndPowerUp();
            }
        }
    }

    // Llamado desde fuera (ej: inventario) cuando se obtiene el power up
    public void GiveSpeedPowerUp()
    {
        hasPowerUp = true;
        Debug.Log("PowerUp de velocidad recibido.");
    }

    private void ActivatePowerUp()
    {
        isActive = true;
        hasPowerUp = false; // Se consume
        timer = powerUpDuration;
        playerController.playerSpeed = originalSpeed * speedMultiplier;
        Debug.Log("¡PowerUp de velocidad activado!");
    }

    private void EndPowerUp()
    {
        isActive = false;
        playerController.playerSpeed = originalSpeed;
        Debug.Log("PowerUp de velocidad finalizado.");
    }
}

