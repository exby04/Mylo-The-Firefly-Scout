using UnityEngine;
using UnityEngine.UI;

public class PanelCambioPowerUpUI : MonoBehaviour
{
    public static PanelCambioPowerUpUI instance;

    public GameObject panel;
    public Image imagenActual;
    public Image imagenNuevo;

    private string powerUpActual;
    private string powerUpNuevo;

    public HoverBrilloUI PowerUpActualHover;
    public HoverBrilloUI PowerUpNuevoHover;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        panel.SetActive(false);
    }

    public void MostrarPanel(string actual, string nuevo, Sprite spriteActual, Sprite spriteNuevo)
    {
        powerUpActual = actual;
        powerUpNuevo = nuevo;

        imagenActual.sprite = spriteActual;
        imagenNuevo.sprite = spriteNuevo;

        panel.SetActive(true);
    }

    public void SeleccionarActual()
    {
        PowerUpActualHover?.DesactivarBrillo();
        PowerUpNuevoHover?.DesactivarBrillo();

        Inventario.instance.RecogerPowerUp(powerUpActual);
        panel.SetActive(false);
    }


    public void SeleccionarNuevo()
    {
        PowerUpActualHover?.DesactivarBrillo();
        PowerUpNuevoHover?.DesactivarBrillo();

        Inventario.instance.RecogerPowerUp(powerUpNuevo);
        panel.SetActive(false);
    }
}
