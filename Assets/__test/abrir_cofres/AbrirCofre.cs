using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbrirCofre : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;
    private static bool llaveObtenida = false;

    // Referencias opcionales para actualizar otras partes del HUD
    public GameObject mensajeUI;
    public Image powerUpImagen;
    public Sprite powerUpLuz;       // Sprite para power-up "Vision" (opcional si se usa en Inventario)
    public Sprite powerUpRapidez;   // Sprite para power-up "Velocidad" (opcional si se usa en Inventario)

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

        // Primero, probamos obtener la llave (con probabilidad 1/7)
        if (!llaveObtenida && Random.Range(1, 8) == 1)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado una llave!");
        }
        else
        {
            // Determinar aleatoriamente el nuevo power-up: 0 = Velocidad, 1 = Vision
            int random = Random.Range(0, 2);
            string nuevoPowerUp = (random == 0) ? "Velocidad" : "Vision";

            // Si el jugador ya tiene un power-up, mostramos el panel de intercambio
            if (Inventario.instance.ObtenerPowerUp() != null)
            {
                PanelCambioPowerUpUI.instance.MostrarPanel(nuevoPowerUp);
                Debug.Log("Mostrando panel de cambio para power-up.");
            }
            else
            {
                // Si no tiene un power-up, se recoge directamente y se muestra el mensaje
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
