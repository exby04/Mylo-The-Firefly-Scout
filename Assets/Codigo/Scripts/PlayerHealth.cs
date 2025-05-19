using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerHealth : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int maxLives = 3;
    private int currentLives;

    public Image[] heartImages;
    private bool isHidden = false;
    private bool isDead = false;

    private Animator animator;
    private PlayerController controller;

    void Start()
    {
        currentLives = maxLives;
        UpdateHUD();

        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        if (isHidden || isDead)
            return;

        currentLives -= damage;
        if (currentLives < 0) currentLives = 0;


        UpdateHUD();

        if (currentLives == 0)
        {
            isDead = true;

            // Animación muerte
            if (animator != null)
                animator.SetTrigger("Die");

            
            if (controller != null)
                controller.puedeMover = false;

            StartCoroutine(EsperarAntesDeMorir());
        }
    }

    IEnumerator EsperarAntesDeMorir()
    {
        yield return new WaitForSeconds(4f); 

        if (CrossfadeManager.Instance != null)
            CrossfadeManager.Instance.FadeThroughScenes("DeathScene", 5f, "DefeatScene");
    }

    void UpdateHUD()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = (i < currentLives);
        }
    }

    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
    }
}
