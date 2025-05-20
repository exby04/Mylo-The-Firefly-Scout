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

[Header("Luz del tronco")]
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

        if (troncoLight != null)
            troncoLight.enabled = false;
     
        if (salirTagUI != null)
            salirTagUI.SetActive(false);
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

        if (salirTagUI != null)
            salirTagUI.SetActive(true);

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
        if (troncoLight != null)
            troncoLight.enabled = false;

        if (salirTagUI != null)
            salirTagUI.SetActive(false);

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