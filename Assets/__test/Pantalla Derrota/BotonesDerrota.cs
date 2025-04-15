using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesDerrota : MonoBehaviour
{
    // Vuelve a cargar la escena actual
    public void VolverAJugar()
    {
        SceneManager.LoadScene("Nivel1"); // Cambia "Nivel1" por el nombre exacto de tu escena
    }

    // Carga la escena de inicio
    public void SalirAlInicio()
    {
        SceneManager.LoadScene("Inicio"); // Cambia "Inicio" por el nombre real de tu escena
    }
}
