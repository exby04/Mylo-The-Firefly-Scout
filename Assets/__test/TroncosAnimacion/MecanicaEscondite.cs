using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    [SerializeField] private float hideDuration = 5f;
    [SerializeField] private float holdTimeToHide = 2f;

    [Header("Animación y efectos")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animTriggerName = "Salir";
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private ParticleSystem particulasPolvo;
    [SerializeField] private float tiempoParticulas = 1f;

    [Header("Salida manual opcional")]
    [SerializeField] private bool permitirSalidaManual = false;

    private bool isUsed = false;
    private bool isHiding = false;
    private bool playerInZone = false;
    private Coroutine hidingCoroutine;

    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    public float HoldDuration => holdTimeToHide;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (animator != null)
            animator.speed = animationSpeed;
    }

    private void Update()
    {
        if (!permitirSalidaManual || !isHiding || !playerInZone)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StopCoroutine(hidingCoroutine);
            ExitHiding();
        }
    }

    public void OnInteract()
    {
        if (isUsed || player == null || isHiding) return;

        hidingCoroutine = StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        isHiding = true;
        isUsed = true;

        if (playerController != null)
            playerController.enabled = false;

        if (playerHealth != null)
            playerHealth.SetHidden(true);

        player.SetActive(false);

        yield return new WaitForSeconds(hideDuration);

        ExitHiding();
    }

    private void ExitHiding()
    {
        isHiding = false;

        //Activar animación
        if (animator != null && !string.IsNullOrEmpty(animTriggerName))
        {
            animator.speed = animationSpeed;
            animator.SetTrigger(animTriggerName);
            StartCoroutine(DetenerAnimacionDespues(animationDuration));
        }

        //Activar partículas
        if (particulasPolvo != null)
        {
            particulasPolvo.gameObject.SetActive(true);
            particulasPolvo.Play();
            StartCoroutine(DesactivarParticulasDespues(tiempoParticulas));
        }

        //Volver a activar el jugador
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
