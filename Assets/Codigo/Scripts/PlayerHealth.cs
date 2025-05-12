using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerHealth : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int maxLives = 3;
    private int currentLives;

    public Image[] heartImages;       
    public GameObject gameOverMenu;   

    private bool isHidden = false;    

    [Header("Escena de Derrota")]
#if UNITY_EDITOR
    public SceneAsset derrotaScene;   
#endif
    [SerializeField] private string derrotaSceneName; 

    void OnValidate()
    {
#if UNITY_EDITOR
        if (derrotaScene != null)
            derrotaSceneName = derrotaScene.name;
#endif
    }

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
            Debug.Log("GameOverMenu no asignado (lo cual está bien si usas escena de derrota).");
        }
    }

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

        Time.timeScale = 1f; // Por si el juego está pausado

        if (!string.IsNullOrEmpty(derrotaSceneName))
        {
            SceneManager.LoadScene(derrotaSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado ninguna escena de derrota.");
            if (gameOverMenu != null)
            {
                gameOverMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
    }
}
