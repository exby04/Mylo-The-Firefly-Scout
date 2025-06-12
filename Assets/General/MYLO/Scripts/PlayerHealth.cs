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
    private MyloAudio myloAudio;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoLatido;
    [SerializeField] private AudioSource audioSourceUI;
    private bool latidoActivo = false;

    void Start()
    {
        currentLives = maxLives;
        UpdateHUD();

        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        myloAudio = GetComponent<MyloAudio>();
    }

    public void TakeDamage(int damage)
    {
        if (isHidden || isDead)
            return;

        currentLives -= damage;
        if (currentLives < 0) currentLives = 0;

        if (myloAudio != null)
            myloAudio.SonidoDañoAleatorio();


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
            bool heartShouldBeVisible = (i < currentLives);

            if (heartImages[i].enabled && !heartShouldBeVisible)
            {
                // Se ha perdido este corazón
                Animator heartAnimator = heartImages[i].GetComponent<Animator>();
                if (heartAnimator != null)
                {
                    heartAnimator.SetTrigger("Lost");
                }

                StartCoroutine(DesactivarCorazonTrasDelay(heartImages[i], 0.10f));
            }
            else if (heartShouldBeVisible)
            {
                // Mostrar el corazón (por si se ha curado)
                heartImages[i].enabled = true;

                // Si es el último corazón visible, activar "Latido"
                if (currentLives == 1 && i == 0) // o usa i == heartImages.Length - 1 si tu último corazón es el derecho
                {
                    Animator heartAnimator = heartImages[i].GetComponent<Animator>();
                    if (heartAnimator != null)
                    {
                        heartAnimator.SetTrigger("Latido");
                    }

                    AudioSource heartAudio = heartImages[i].GetComponent<AudioSource>();
                    if (heartAudio != null && !heartAudio.isPlaying)
                    {
                        heartAudio.loop = true; // por si acaso
                        heartAudio.Play();
                    }

                    latidoActivo = true;
                }
                else
                {
                    Animator heartAnimator = heartImages[i].GetComponent<Animator>();
                    if (heartAnimator != null)
                    {
                        heartAnimator.ResetTrigger("Latido");
                    }

                    if (latidoActivo)
                    {
                        AudioSource heartAudio = heartImages[i].GetComponent<AudioSource>();
                        if (heartAudio != null && heartAudio.isPlaying)
                        {
                            heartAudio.Stop(); // Detiene el latido si ya no estamos en 1 vida
                        }

                        latidoActivo = false;
                    }
                }

            }
        }
    }


    IEnumerator DesactivarCorazonTrasDelay(Image heart, float delay)
    {
        yield return new WaitForSeconds(delay);
        heart.enabled = false;
    }




    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
    }
}
