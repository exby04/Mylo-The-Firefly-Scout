using UnityEngine;
using UnityEngine.UI;

public class AbrirCofre : MonoBehaviour, IInteractable
{
    public GameObject cofreAbiertoPrefab;
    private bool abierto = false;
    private static bool llaveObtenida = false;
    private static int cofresAbiertos = 0;

    public GameObject mensajeUI;
    public Image powerUpImagen;
    public Sprite powerUpLuz;
    public Sprite powerUpRapidez;

    public float tiempoParaAbrir = 2f; // <-- NUEVO: tiempo de mantener E

    private void Start()
    {
        cofresAbiertos = 0;
    }

    // ✅ Implementación de la interfaz
    public float HoldDuration => tiempoParaAbrir;

    public void OnInteract()
    {
        if (abierto) return;

        Abrir();
    }

    void Abrir()
    {
        abierto = true;

        Instantiate(cofreAbiertoPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        mensajeUI.SetActive(true);

        cofresAbiertos++;

        if (!llaveObtenida && cofresAbiertos == 4)
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
                nuevoPowerUp = "Vision";
            else if (actual == "Vision")
                nuevoPowerUp = "Velocidad";
            else
                nuevoPowerUp = (Random.Range(0, 2) == 0) ? "Velocidad" : "Vision";

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
}
