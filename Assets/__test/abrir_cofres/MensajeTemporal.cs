using UnityEngine;

public class MensajeTemporal : MonoBehaviour
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
