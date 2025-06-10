using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    [SerializeField] private float hideDuration = 5f;
    [SerializeField] private float holdTimeToHide = 2f;

    [Header("Animación y efectos")]
    [SerializeField] private Animator animator; // Animator del escondite
    [SerializeField] private string animTriggerEsconderse = "Esconderse";
    [SerializeField] private string animTriggerSalir = "Salir";
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private ParticleSystem particulasPolvo;
    [SerializeField] private float tiempoParticulas = 1f;

    [Header("Animación del jugador")]
    [SerializeField] private string playerHideTrigger = "Esconderse";

    [Header("Salida manual opcional")]
    [SerializeField] private bool permitirSalidaManual = false;

    [Header("Luz del tronco")]
    [SerializeField] private Light troncoLightOverhead;
    [SerializeField] private Light troncoLight;



    [Header("UI - Tag de Salir")]
    [SerializeField] private GameObject salirTagUI;


    private bool isUsed = false;
    private bool isHiding = false;
    private bool playerInZone = false;
    private Coroutine hidingCoroutine;

    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private Animator playerAnimator;

    public float HoldDuration => holdTimeToHide;

    [SerializeField] private AudioClip sonidoRomper;
    private AudioSource audioSource;


    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
            playerAnimator = player.GetComponent<Animator>();
        }

        if (animator != null)
            animator.speed = animationSpeed;

        if (troncoLight != null)
            troncoLight.enabled = false;

        if (troncoLightOverhead != null)
            troncoLightOverhead.enabled = false;

        if (salirTagUI != null)
            salirTagUI.SetActive(false);

        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (!permitirSalidaManual || !isHiding)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hidingCoroutine != null)
            {
                StopCoroutine(hidingCoroutine);
                hidingCoroutine = null;
            }

            ExitHiding();
        }
    }

    public void OnInteract()
    {
        if (player == null) return;

        if (isHiding && permitirSalidaManual)
        {
            StopCoroutine(hidingCoroutine);
            ExitHiding();
            return;
        }

        if (isUsed || isHiding) return;

        hidingCoroutine = StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        isHiding = true;
        isUsed = true;

        if (troncoLight != null)
            troncoLight.enabled = true;

        if (troncoLightOverhead != null)
            troncoLightOverhead.enabled = true;

        if (salirTagUI != null)
            salirTagUI.SetActive(true);

        if (salirTagUI != null)
            salirTagUI.SetActive(true);

        // Animación del escondite
        if (animator != null && HasTrigger(animator, animTriggerEsconderse))
        {
            Debug.Log("[HideSpot] Activando trigger del escondite: " + animTriggerEsconderse);
            animator.speed = animationSpeed;
            animator.SetTrigger(animTriggerEsconderse);
        }

        // Animación del jugador
        if (playerAnimator != null && HasTrigger(playerAnimator, playerHideTrigger))
        {
            Debug.Log("[HideSpot] Activando trigger del jugador: " + playerHideTrigger);
            playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            playerAnimator.SetTrigger(playerHideTrigger);
        }

        yield return new WaitForSeconds(0.5f);

        if (playerController != null)
            playerController.enabled = false;

        if (playerHealth != null)
            playerHealth.SetHidden(true);

        player.SetActive(false);

        yield return new WaitForSeconds(hideDuration - 0.5f);

        ExitHiding();
    }

    private void ExitHiding()
    {
        isHiding = false;

        if (troncoLight != null)
            troncoLight.enabled = false;

        if (troncoLightOverhead != null)
            troncoLightOverhead.enabled = false;

        if (salirTagUI != null)
            salirTagUI.SetActive(false);

        if (audioSource != null && sonidoRomper != null)
            audioSource.PlayOneShot(sonidoRomper);

        // Animación de salida
        if (animator != null && HasTrigger(animator, animTriggerSalir))
        {
            Debug.Log("[HideSpot] Activando trigger de salida: " + animTriggerSalir);
            animator.speed = animationSpeed;
            animator.SetTrigger(animTriggerSalir);
            StartCoroutine(DetenerAnimacionDespues(animationDuration));
        }

        // Partículas
        if (particulasPolvo != null)
        {
            particulasPolvo.gameObject.SetActive(true);
            particulasPolvo.Play();
            StartCoroutine(DesactivarParticulasDespues(tiempoParticulas));
        }

        player.SetActive(true);

        if (playerController != null)
            playerController.enabled = true;

        if (playerHealth != null)
            playerHealth.SetHidden(false);
    }

    private IEnumerator DetenerAnimacionDespues(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        if (animator != null)
            animator.speed = 0f;
    }

    private IEnumerator DesactivarParticulasDespues(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        if (particulasPolvo != null)
            particulasPolvo.Stop();
    }

    private bool HasTrigger(Animator anim, string triggerName)
    {
        foreach (var param in anim.parameters)
        {
            if (param.name == triggerName && param.type == AnimatorControllerParameterType.Trigger)
                return true;
        }
        Debug.LogWarning("[HideSpot] El Animator no tiene el trigger: " + triggerName);
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInZone = false;
    }
}
