using UnityEngine;
using System.Collections;

public class FadeInDelay : MonoBehaviour
{
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1f); // espera 2 segundos
        animator.SetTrigger("StartFade"); 
    }

}
