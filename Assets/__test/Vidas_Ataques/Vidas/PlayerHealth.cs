using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int maxLives = 3;
    private int currentLives;
    public Image[] heartImages;   // Asigna las imágenes de los corazones desde el Inspector.
    public GameObject gameOverMenu;   // Asigna el Panel del Game Over (desactivado al inicio).
    private bool isHidden = false;      // Actualizado desde otros scripts, por ejemplo, de escondites.

    void Start()
    {
        currentLives = maxLives;
        UpdateHUD();

        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
            Debug.Log("GameOverMenu desactivado al iniciar.");
        }
        else
        {
            Debug.LogWarning("GameOverMenu no asignado en el Inspector.");
        }
    }

    // Eliminamos el código de Update que simulaba daño con la tecla T.
    // Ahora, TakeDamage solo se llama desde otros scripts (como el ataque de murciélagos).

    public void TakeDamage(int damage)
    {
        if (isHidden)
        {
            Debug.Log("Ataque ignorado: el jugador está escondido.");
            return;
        }

        currentLives -= damage;
        if (currentLives < 0)
            currentLives = 0;

        Debug.Log("Vida perdida. Vidas restantes: " + currentLives);
        UpdateHUD();

        if (currentLives == 0)
        {
            GameOver();
        }
    }

    void UpdateHUD()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = (i < currentLives);
        }
    }

    void GameOver()
    {
        Debug.Log("¡Game Over! El jugador ha perdido todas sus vidas.");
        if (gameOverMenu != null)
            gameOverMenu.SetActive(true);

        // Congela el juego
        Time.timeScale = 0f;
    }


    public void RestartGame()
    {
        Time.timeScale = 1f; // Reinicia el valor del timeScale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Permite que otros scripts actualicen si el jugador está protegido (por ejemplo, en escondites)
    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
    }
}

