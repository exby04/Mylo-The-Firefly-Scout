using UnityEngine;

public class MensajeTemporal_powerup : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Ocultar", 2f);  // Ocultar mensaje en 2 segundos
    }

    void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
