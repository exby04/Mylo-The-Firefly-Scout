using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbrirCofre : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;
    private static bool llaveObtenida = false;

    // Nuevo contador estático para los cofres abiertos
    private static int cofresAbiertos = 0;

    // Referencias opcionales para actualizar otras partes del HUD
    public GameObject mensajeUI;
    public Image powerUpImagen;
    public Sprite powerUpLuz;       
    public Sprite powerUpRapidez;   

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
        if (cofresAbiertos == 7 && !llaveObtenida)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado la llave por obligación!");
        }
        else if (!llaveObtenida && Random.Range(1, 8) == 1)  // Si no es el 7mo cofre, 1/7 de probabilidad
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado una llave!");
        }
        else
        {
            // Obtener el power-up actual del jugador
            string actual = Inventario.instance.ObtenerPowerUp();
            string nuevoPowerUp;

            // Evitar repetir el mismo power-up
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
                // Si no tiene ningún power-up, elegir aleatoriamente
                int random = Random.Range(0, 2);
                nuevoPowerUp = (random == 0) ? "Velocidad" : "Vision";
            }

            // Si ya tiene un power-up, mostrar el panel de cambio
            if (actual != null)
            {
                PanelCambioPowerUpUI.instance.MostrarPanel(nuevoPowerUp);
                Debug.Log("Mostrando panel de cambio para power-up.");
            }
            else
            {
                // Si no tiene ninguno, recoger directamente
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
