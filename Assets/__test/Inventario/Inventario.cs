using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;

    private string powerUpActual = null;
    private bool tieneLlave = false;

    public Image keyImage;         // Imagen de la llave en el HUD
    public Image powerUpImage;     // Imagen del Power-Up en el HUD
    public GameObject mensajeUI;   // GameObject del mensaje (dentro del HUD)
    public Image mensajeImagen;    // Imagen que se muestra en el mensaje
    public TextMeshProUGUI mensajeTexto; // Texto del mensaje

    public Sprite llaveSprite;          // Sprite de la llave obtenida
    public Sprite powerUpLuzSprite;     // Sprite para Power-Up de Visión
    public Sprite powerUpRapidezSprite; // Sprite para Power-Up de Velocidad

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Método para recoger la llave (siempre se muestra el mensaje)
    public void RecogerLlave()
    {
        tieneLlave = true;
        keyImage.sprite = llaveSprite;  // Actualiza la imagen de la llave en el HUD

        // Mostrar mensaje de llave
        mensajeImagen.sprite = llaveSprite;
        mensajeTexto.text = "¡Has obtenido una llave!";
        mensajeUI.SetActive(true);
    }

    // Método para recoger un power-up (se muestra el mensaje solo si se asigna directamente)
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

        mensajeUI.SetActive(true);
    }

    // Método para obtener el power-up actual (devuelve "Velocidad", "Vision" o null)
    public string ObtenerPowerUp()
    {
        return powerUpActual;
    }

    // Método para intercambiar el power-up actual por uno nuevo
    public void IntercambiarPowerUp(string nuevoPowerUp)
    {
        // Simplemente se reemplaza el actual por el nuevo
        RecogerPowerUp(nuevoPowerUp);
        Debug.Log("Power-up intercambiado por: " + nuevoPowerUp);
    }

    // Retorna si el jugador tiene la llave
    public bool TieneLlave()
    {
        return tieneLlave;
    }
}
