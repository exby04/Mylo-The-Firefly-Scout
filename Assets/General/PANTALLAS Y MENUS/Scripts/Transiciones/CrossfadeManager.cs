using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrossfadeManager : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;

    public static CrossfadeManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        animator.SetTrigger("Start"); 
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        yield return null;
        animator.SetTrigger("End");
    }

    
    public void FadeThroughScenes(string firstScene, float waitTime, string finalScene)
    {
        StartCoroutine(FadeSequence(firstScene, waitTime, finalScene));
    }

    private IEnumerator FadeSequence(string scene1, float delay, string scene2)
    {
        Debug.Log("FADE OUT TO FIRST SCENE");
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        Debug.Log("LOADING SCENE: " + scene1);
        SceneManager.LoadScene(scene1);
        yield return null;

        Debug.Log("FADE IN FIRST SCENE");
        animator.SetTrigger("End");
        yield return new WaitForSeconds(delay);

        Debug.Log("FADE OUT TO FINAL SCENE");
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        Debug.Log("LOADING FINAL SCENE: " + scene2);
        SceneManager.LoadScene(scene2);
        yield return null;

        Debug.Log("FADE IN FINAL SCENE");
        animator.SetTrigger("End");
    }

}
