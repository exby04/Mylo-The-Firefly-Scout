using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Animator fadeAnimator;
    public string nextScene;

    public void StartFadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    // Llamado desde el evento de animación FadeOut
    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(nextScene);
    }
}
