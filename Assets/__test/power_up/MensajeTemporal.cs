using UnityEngine;

public class MensajeTemporal_powerup : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Ocultar", 2f);  
    }

    void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
