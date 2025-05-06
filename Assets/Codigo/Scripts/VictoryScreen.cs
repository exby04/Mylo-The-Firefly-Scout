using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VictoryScreen : MonoBehaviour
{
    [Header("Scene Settings")]
#if UNITY_EDITOR
    public SceneAsset levelSceneAsset;
    public SceneAsset menuSceneAsset;
#endif

    [SerializeField] private string levelSceneName;
    [SerializeField] private string menuSceneName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (levelSceneAsset != null)
            levelSceneName = levelSceneAsset.name;

        if (menuSceneAsset != null)
            menuSceneName = menuSceneAsset.name;
    }
#endif

    public void RestartGame()
    {
        Debug.Log("Botón 'Volver a jugar' pulsado");
        if (!string.IsNullOrEmpty(levelSceneName))
        {
            SceneManager.LoadScene(levelSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la escena del nivel.");
        }
    }

    public void ReturnToMenu()
    {
        Debug.Log("Botón 'Salir al inicio' pulsado");
        if (!string.IsNullOrEmpty(menuSceneName))
        {
            SceneManager.LoadScene(menuSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la escena del menú.");
        }
    }
}
