using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventario : MonoBehaviour
{
    public static Inventario instance; // Singleton para fácil acceso

    private string powerUpActual = null;
    private bool tieneLlave = false;

    public Text powerUpText; // Referencia al HUD
    public Text llaveText; // Referencia al HUD

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RecogerLlave()
    {
        tieneLlave = true;
        llaveText.text = "LLAVE"; // Actualiza el HUD
    }

    public void RecogerPowerUp(string powerUp)
    {
        if (powerUpActual == null)
        {
            powerUpActual = powerUp;
        }
        else
        {
            // Cambia el power-up si ya tenía uno
            powerUpActual = powerUp;
        }

        powerUpText.text = powerUpActual; // Actualiza el HUD
    }

    public bool UsarPowerUp()
    {
        if (powerUpActual != null)
        {
            Debug.Log("Usando Power-Up: " + powerUpActual);
            powerUpActual = null;
            powerUpText.text = "Power-Up: Ninguno";
            return true;
        }
        return false;
    }

    public bool TieneLlave()
    {
        return tieneLlave;
    }
}

