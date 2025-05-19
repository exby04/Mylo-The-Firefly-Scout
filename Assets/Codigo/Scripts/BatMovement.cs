using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float baseSpeed = 5f;
    public float maxSpeed = 12f;
    public float acceleration = 1f;
    public float attackDistance = 1f;

    private float currentSpeed;
    private bool isAttacking = false;

    private Transform player;
    private GameObject playerObj;
    private Animator playerAnimator;

    [Header("Animación del jugador al ser detectado/golpeado")]
    public string triggerAnimacionJugador = "GetAttacked";
    public float duracionAnimacionGolpeado = 60f;

    private bool hasAlreadyTriggered = false;

    public void FlyToAttack(GameObject targetPlayer)
    {
        if (targetPlayer != null)
        {
            playerObj = targetPlayer;
            player = targetPlayer.transform;
            playerAnimator = targetPlayer.GetComponentInChildren<Animator>();

            if (playerAnimator == null)
                Debug.LogWarning("No se encontró Animator en el jugador o sus hijos.");

            isAttacking = true;
            currentSpeed = baseSpeed;
        }
        else
        {
            Debug.LogWarning("Referencia al jugador nula. No se puede iniciar el ataque.");
        }
    }

    void Update()
    {
        if (!isAttacking || player == null || playerObj == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            isAttacking = false;

            if (!playerObj.activeInHierarchy)
            {
                Debug.Log("El murciélago llegó, pero el jugador está escondido.");
            }
            else
            {
                Debug.Log("El murciélago ataca al jugador.");

                PlayerHealth ph = playerObj.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(1);
                }

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
                Debug.Log("Jugador detectado por trigger. Animación activada.");
            }
            else
            {
                Debug.LogWarning("No se encontró Animator en el jugador.");
            }

            if (!isAttacking)
            {
                isAttacking = true;
                currentSpeed = baseSpeed;
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
        yield return new WaitForSeconds(0.3f);
        Vector3 exitPoint = transform.position + new Vector3(0f, 10f, 0f);

        while (Vector3.Distance(transform.position, exitPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPoint, baseSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
