using UnityEngine;

public class PruebaPowerUpLuz : MonoBehaviour
{
    public PowerUpLuz powerUp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (powerUp != null)
            {
                powerUp.Activar();
            }
        }
    }
}
