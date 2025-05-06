using UnityEngine;

public class MensajeTemporal : MonoBehaviour
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
