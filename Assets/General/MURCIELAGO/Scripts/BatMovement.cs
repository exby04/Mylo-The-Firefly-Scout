using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float alturaImpacto = 1.5f;

    private float currentSpeed;
    private bool isAttacking = false;

    private Transform player;
    private GameObject playerObj;
    private Animator playerAnimator;
    private Animator selfAnimator;

    [Header("Animación del murciélago")]
    [SerializeField] private string nombreAnimacionAtaque = "Take 001";
    [SerializeField] private float velocidadAnimacion = 1f;

    [Header("Animación del jugador al ser golpeado")]
    [SerializeField] private string triggerAnimacionJugador = "GetAttacked";
    [SerializeField] private float duracionAnimacionGolpeado = 60f;


    //---------
    [Header("Efecto de nube al impactar")]
    [SerializeField] private GameObject efectoNubePrefab;
    [SerializeField] private Vector3 offsetImpacto = new Vector3(0, 1.2f, 0);
    //---------

    private bool hasAlreadyTriggered = false;
    [Header("Sonidos")]
    [SerializeField] private AudioSource flyingAudioSource;
    [SerializeField] private AudioSource attackAudioSource;

    void Start()
    {
        selfAnimator = GetComponent<Animator>();
        if (selfAnimator != null)
        {
            selfAnimator.speed = velocidadAnimacion;
            selfAnimator.Play(nombreAnimacionAtaque); // animación al comenzar
        }
    }

    public void FlyToAttack(GameObject targetPlayer)
    {
        if (targetPlayer != null)
        {
            playerObj = targetPlayer;
            player = playerObj.transform;
            playerAnimator = playerObj.GetComponentInChildren<Animator>();

            if (playerAnimator == null)
                UnityEngine.Debug.LogWarning("No se encontró Animator en el jugador o sus hijos.");

            isAttacking = true;
            if (flyingAudioSource != null)
            {
              flyingAudioSource.loop = true;
              flyingAudioSource.Play();
            }

            currentSpeed = baseSpeed;

            if (selfAnimator != null)
            {
                selfAnimator.Play(nombreAnimacionAtaque);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Referencia al jugador nula. No se puede iniciar el ataque.");
        }
    }

    void Update()
    {
        if (!isAttacking || player == null || playerObj == null)
            return;

        Vector3 objetivoAtaque = player.position + new Vector3(0, alturaImpacto, 0);
        float distance = Vector3.Distance(transform.position, objetivoAtaque);

        if (distance > attackDistance)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            transform.position = Vector3.MoveTowards(transform.position, objetivoAtaque, currentSpeed * Time.deltaTime);

            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f;

            if (directionToPlayer.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer.normalized);
                Vector3 euler = lookRotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0, euler.y, 0);
            }
        }
        else
        {
            isAttacking = false;

            if (!playerObj.activeInHierarchy)
            {
                UnityEngine.Debug.Log("El murciélago llegó, pero el jugador está escondido.");
            }
            else
            {
                UnityEngine.Debug.Log("El murciélago ataca al jugador.");

                PlayerHealth ph = playerObj.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(1);
                    if (flyingAudioSource != null)
                     flyingAudioSource.Stop();

                    if (attackAudioSource != null)
                     attackAudioSource.Play();

                }

                //Instanciar la nubecita de impacto
                if (efectoNubePrefab != null)
                {
                    Vector3 posicionImpacto = player.position + offsetImpacto;
                    Instantiate(efectoNubePrefab, posicionImpacto, Quaternion.identity);
                }
                //--------------

                if (!hasAlreadyTriggered)
                {
                    hasAlreadyTriggered = true;
                    StartCoroutine(ReproducirAnimacionYDesactivarMovimiento());
                }
            }

            StartCoroutine(FlyAway());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasAlreadyTriggered && other.CompareTag("Player"))
        {
            hasAlreadyTriggered = true;

            playerObj = other.gameObject;
            player = playerObj.transform;
            playerAnimator = playerObj.GetComponentInChildren<Animator>();

            if (playerAnimator != null)
            {
                StartCoroutine(ReproducirAnimacionYDesactivarMovimiento());
                UnityEngine.Debug.Log("Jugador detectado por trigger. Animación activada.");
            }
            else
            {
                UnityEngine.Debug.LogWarning("No se encontró Animator en el jugador.");
            }

            if (!isAttacking)
            {
                isAttacking = true;
                currentSpeed = baseSpeed;

                if (selfAnimator != null)
                {
                    selfAnimator.Play(nombreAnimacionAtaque);
                }
            }
        }
    }

    IEnumerator ReproducirAnimacionYDesactivarMovimiento()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(triggerAnimacionJugador);
        }

        var controller = playerObj.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
            yield return new WaitForSeconds(duracionAnimacionGolpeado);
            controller.enabled = true;
        }
    }

    IEnumerator FlyAway()
    {
        if (flyingAudioSource != null && flyingAudioSource.isPlaying)
        flyingAudioSource.Stop();

        yield return new WaitForSeconds(0.3f);

        Vector3 awayDirection = transform.position - player.position;
        awayDirection.y = 0f;
        awayDirection = awayDirection.normalized;

        Vector3 escapeDirection = (awayDirection + Vector3.up).normalized;
        Vector3 exitPoint = transform.position + escapeDirection * 10f;


        Vector3 lookDir = new Vector3(escapeDirection.x, 0f, escapeDirection.z);
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDir.normalized);
            Vector3 euler = targetRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, euler.y, 0);
        }

        while (Vector3.Distance(transform.position, exitPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPoint, baseSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
