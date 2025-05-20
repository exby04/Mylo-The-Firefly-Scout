using UnityEngine;
using System.Collections;

public class FadeInDelay : MonoBehaviour
{
    public Animator animator;


    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1f); 
        animator.SetTrigger("StartFade"); 
    }

}
