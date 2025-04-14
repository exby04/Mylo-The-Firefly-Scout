using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;

    private string powerUpActual = null;
    private bool tieneLlave = false;

    [Header("HUD")]
    public Image keyImage;
    public Image powerUpImage;
    public GameObject mensajeUI;
    public Image mensajeImagen;
    public TextMeshProUGUI mensajeTexto;

    [Header("Sprites")]
    public Sprite llaveSprite;
    public Sprite powerUpLuzSprite;
    public Sprite powerUpRapidezSprite;
    public Sprite powerUpVacioSprite; // ✅ Nuevo sprite para slot vacío

    [Header("Referencias de scripts")]
    public PowerUpLuz powerUpLuz;
    public SpeedPowerUpController speedPowerUpController;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivarPowerUp();
        }
    }

    public void RecogerLlave()
    {
        tieneLlave = true;
        keyImage.sprite = llaveSprite;
        keyImage.color = Color.white;

        mensajeImagen.sprite = llaveSprite;
        mensajeTexto.text = "¡Has obtenido una llave!";
        mensajeUI.SetActive(true);
    }

    public void RecogerPowerUp(string powerUp)
    {
        powerUpActual = powerUp;

        if (powerUp == "Velocidad")
        {
            powerUpImage.sprite = powerUpRapidezSprite;
            mensajeImagen.sprite = powerUpRapidezSprite;
            mensajeTexto.text = "¡Has obtenido un Power-Up de Velocidad!";
        }
        else if (powerUp == "Vision")
        {
            powerUpImage.sprite = powerUpLuzSprite;
            mensajeImagen.sprite = powerUpLuzSprite;
            mensajeTexto.text = "¡Has obtenido un Power-Up de Visión!";
        }

        powerUpImage.color = Color.white;
        mensajeUI.SetActive(true);
    }

    public string ObtenerPowerUp()
    {
        return powerUpActual;
    }

    public void IntercambiarPowerUp(string nuevoPowerUp)
    {
        RecogerPowerUp(nuevoPowerUp);
        Debug.Log("Power-up intercambiado por: " + nuevoPowerUp);
    }

    public bool TieneLlave()
    {
        return tieneLlave;
    }

    public void ActivarPowerUp()
    {
        if (powerUpActual == null) return;

        switch (powerUpActual)
        {
            case "Vision":
                if (powerUpLuz != null)
                {
                    powerUpLuz.Activar();
                    Debug.Log("PowerUp de Visión activado");
                }
                break;

            case "Velocidad":
                if (speedPowerUpController != null)
                {
                    speedPowerUpController.GiveSpeedPowerUp();
                    Debug.Log("PowerUp de Velocidad activado");
                }
                break;

            default:
                Debug.LogWarning("Power-up desconocido: " + powerUpActual);
                return;
        }

        // ✅ Reemplazar el ícono con el sprite vacío en lugar de ocultarlo
        powerUpActual = null;
        powerUpImage.sprite = powerUpVacioSprite;
        powerUpImage.color = Color.white;
    }
}
