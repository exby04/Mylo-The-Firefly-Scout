using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelCambioPowerUpUI : MonoBehaviour
{
    public static PanelCambioPowerUpUI instance;

    public GameObject panelCambio;           
    public TextMeshProUGUI mensajeTexto;       
    public Button botonAceptar;                 
    public Button botonCancelar;              

    private string nuevoPowerUp;               

    private void Awake()
    {
        if (instance == null)
            instance = this;
        panelCambio.SetActive(false); 
    }

   
    public void MostrarPanel(string nuevoPowerUp)
    {
        this.nuevoPowerUp = nuevoPowerUp;
        string actual = Inventario.instance.ObtenerPowerUp();

        //mensajeTexto.text = $"¡Has obtenido un Power-Up de {nuevoPowerUp}!\n¿Cambiar '{actual}' por '{nuevoPowerUp}'?";
        mensajeTexto.text = $"¿Debería cambiar '{actual}' por '{nuevoPowerUp}'?";
        panelCambio.SetActive(true);

        Time.timeScale = 0f;//Pausar juego

        botonAceptar.onClick.RemoveAllListeners();
        botonCancelar.onClick.RemoveAllListeners();

        botonAceptar.onClick.AddListener(AceptarCambio);
        botonCancelar.onClick.AddListener(CancelarCambio);
    }

    void AceptarCambio()
    {
        Inventario.instance.IntercambiarPowerUp(nuevoPowerUp);
        panelCambio.SetActive(false);

        Time.timeScale = 1f; //Reanudar juego
    }

    void CancelarCambio()
    {
        panelCambio.SetActive(false);
        Time.timeScale = 1f; //Reanudar juego
    }
}
