using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;

    private string powerUpActual = null;
    private bool tieneLlave = false;

    public Image keyImage;  // Imagen de la llave en el HUD
    public Image powerUpImage;  // Imagen del Power-Up en el HUD
    public GameObject mensajeUI;  // Objeto que contiene el mensaje
    public Image mensajeImagen;  // Imagen dentro del mensaje
    public TextMeshProUGUI mensajeTexto;  // Texto dentro del mensaje

    public Sprite llaveSprite;  // Imagen cuando se obtiene la llave
    public Sprite powerUpLuzSprite;  // Imagen de Power-Up Luz
    public Sprite powerUpRapidezSprite;  // Imagen de Power-Up Rapidez

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RecogerLlave()
    {
        tieneLlave = true;
        keyImage.sprite = llaveSprite;  // Cambia la imagen de la llave en el HUD

        // Mostrar mensaje de que encontró una llave
        mensajeImagen.sprite = llaveSprite;
        mensajeTexto.text = "¡Has obtenido una llave!";
        mensajeUI.SetActive(true);
    }

    public void RecogerPowerUp(string powerUp)
    {
        powerUpActual = powerUp;

        // Cambia la imagen del Power-Up en el HUD
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

        mensajeUI.SetActive(true); // Mostrar el mensaje
    }
}
