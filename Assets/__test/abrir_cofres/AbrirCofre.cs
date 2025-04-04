using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbrirCofre : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;

    private static bool llaveObtenida = false;

    // Referencias al HUD
    public GameObject mensajeUI;
    public TextMeshProUGUI mensajeTexto;
    public Image mensajeImagen;

    public Sprite imagenLlave;
    public Sprite imagenVelocidad;
    public Sprite imagenVision;

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

        if (!llaveObtenida && Random.Range(1, 8) == 1)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();

            // Mostrar mensaje
            mensajeTexto.text = "¡Has obtenido una llave!";
            mensajeImagen.sprite = imagenLlave;
            mensajeUI.SetActive(true);
        }
        else
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                Inventario.instance.RecogerPowerUp("Velocidad");

                mensajeTexto.text = "¡Has obtenido velocidad!";
                mensajeImagen.sprite = imagenVelocidad;
                mensajeUI.SetActive(true);
            }
            else
            {
                Inventario.instance.RecogerPowerUp("Vision");

                mensajeTexto.text = "¡Has obtenido visión!";
                mensajeImagen.sprite = imagenVision;
                mensajeUI.SetActive(true);
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
