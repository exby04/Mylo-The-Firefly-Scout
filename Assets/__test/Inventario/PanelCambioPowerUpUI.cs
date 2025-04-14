using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelCambioPowerUpUI : MonoBehaviour
{
    public static PanelCambioPowerUpUI instance;

    // Referencias del panel de cambio dentro del HUD
    public GameObject panelCambio;             // Panel que se mostrará para decidir el intercambio
    public TextMeshProUGUI mensajeTexto;         // Texto que mostrará la pregunta
    public Button botonAceptar;                  // Botón para aceptar el cambio
    public Button botonCancelar;                 // Botón para cancelar el cambio

    private string nuevoPowerUp;                 // Almacena el nuevo power-up que se encontró

    private void Awake()
    {
        if (instance == null)
            instance = this;
        panelCambio.SetActive(false); // Se oculta el panel por defecto
    }

    // Método para mostrar el panel de cambio
    public void MostrarPanel(string nuevoPowerUp)
    {
        this.nuevoPowerUp = nuevoPowerUp;
        string actual = Inventario.instance.ObtenerPowerUp();
        // Se muestra un mensaje que indica lo obtenido y pregunta si desea cambiar
        mensajeTexto.text = $"¡Has obtenido un Power-Up de {nuevoPowerUp}!\n¿Cambiar '{actual}' por '{nuevoPowerUp}'?";
        panelCambio.SetActive(true);

        // Limpiar y asignar los listeners de los botones
        botonAceptar.onClick.RemoveAllListeners();
        botonCancelar.onClick.RemoveAllListeners();

        botonAceptar.onClick.AddListener(AceptarCambio);
        botonCancelar.onClick.AddListener(CancelarCambio);
    }

    void AceptarCambio()
    {
        Inventario.instance.IntercambiarPowerUp(nuevoPowerUp);
        panelCambio.SetActive(false);
    }

    void CancelarCambio()
    {
        panelCambio.SetActive(false);
    }
}
