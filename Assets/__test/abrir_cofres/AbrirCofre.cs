using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbrirCofre : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;
    private static bool llaveObtenida = false;

    private static int cofresAbiertos = 0;

    public GameObject mensajeUI;
    public Image powerUpImagen;
    public Sprite powerUpLuz;       // Sprite para power-up "Vision"
    public Sprite powerUpRapidez;   // Sprite para power-up "Velocidad"

    void Update()
    {
        if (jugadorCerca && !abierto && Input.GetKeyDown(KeyCode.E))
        {
            Abrir();
        }
    }

    void Abrir()
    {
        abierto = true;

        Instantiate(cofreAbiertoPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        mensajeUI.SetActive(true);

        cofresAbiertos++;  // Aumenta el contador de cofres abiertos

        // Si ya has abierto 7 cofres y no has obtenido la llave, forzar la aparición de la llave
        if (!llaveObtenida && cofresAbiertos == 7)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado la llave por obligación!");
        }
        else if (!llaveObtenida && Random.Range(1, 8) == 1)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado una llave!");
        }
        else
        {
            string actual = Inventario.instance.ObtenerPowerUp();
            string nuevoPowerUp;

            if (actual == "Velocidad")
            {
                nuevoPowerUp = "Vision";
            }
            else if (actual == "Vision")
            {
                nuevoPowerUp = "Velocidad";
            }
            else
            {
                nuevoPowerUp = (Random.Range(0, 2) == 0) ? "Velocidad" : "Vision";
            }

            if (!string.IsNullOrEmpty(actual))
            {
                PanelCambioPowerUpUI.instance.MostrarPanel(nuevoPowerUp);
                Debug.Log("Mostrando panel de cambio para power-up.");
            }
            else
            {
                Inventario.instance.RecogerPowerUp(nuevoPowerUp);
                Debug.Log("¡Has obtenido un Power-Up de " + nuevoPowerUp + "!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
