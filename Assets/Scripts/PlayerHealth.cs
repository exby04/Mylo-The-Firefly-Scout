using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Número máximo de vidas (corazones)
    public int maxLives = 3;
    // Número actual de vidas
    private int currentLives;
    // Array de imágenes de UI que representan los corazones en el HUD
    public Image[] heartImages;

    // Variable para indicar si el jugador está escondido
    private bool isHidden = false;

    // Game Over Menu (Panel) que se activa al morir; asigna este objeto en el Inspector
    public GameObject gameOverMenu;

    void Start()
    {
        // Inicializamos el número de vidas al valor máximo
        currentLives = maxLives;
        UpdateHUD();

        // Desactivamos el menú de Game Over al inicio para que no se muestre
        if (gameOverMenu != null)
            gameOverMenu.SetActive(false);
    }

    void Update()
    {
        // Para pruebas: presionar la tecla T simula un ataque y quita una vida
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Test: Aplicando daño al jugador.");
            TakeDamage(1);
        }
    }

    // Método público para aplicar daño al jugador.
    // Si el jugador está escondido, el ataque falla y no se resta ninguna vida.
    public void TakeDamage(int damage)
    {
        if (isHidden)
        {
            Debug.Log("Ataque fallido: el jugador está escondido.");
            return;
        }

        currentLives -= damage;
        if (currentLives < 0)
            currentLives = 0;

        Debug.Log("Vida perdida. Vidas restantes: " + currentLives);

        UpdateHUD();

        // Si se han perdido todas las vidas, se llama al método GameOver
        if (currentLives == 0)
        {
            GameOver();
        }
    }

    // Actualiza el HUD para mostrar el número de vidas restantes
    void UpdateHUD()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = (i < currentLives);
        }
    }

    // Lógica a ejecutar cuando el jugador se queda sin vidas
    void GameOver()
    {
        Debug.Log("¡Game Over! El jugador ha perdido todas sus vidas.");

        // Activa el menú de Game Over (panel)
        if (gameOverMenu != null)
            gameOverMenu.SetActive(true);
    }

    // Método para reiniciar la partida, asignarlo al botón del menú de Game Over
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para reiniciar la salud del jugador (útil para pruebas o al reiniciar el nivel)
    public void ResetHealth()
    {
        currentLives = maxLives;
        UpdateHUD();
    }

    // Método para actualizar el estado de escondido desde otros scripts (por ejemplo, HideSpot)
    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
    }
}
