using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float baseSpeed = 5f;
    public float maxSpeed = 12f;
    public float acceleration = 1f; // Aumento de velocidad por segundo
    public float attackDistance = 1f;

    private float currentSpeed;
    private bool isAttacking = false;
    private Transform player;
    private GameObject playerObj;

    // Se llama para iniciar el ataque, pasando la referencia al jugador
    public void FlyToAttack(GameObject targetPlayer)
    {
        if (targetPlayer != null)
        {
            playerObj = targetPlayer;
            player = targetPlayer.transform;
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
            // Aumentar velocidad gradualmente hasta maxSpeed
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);

            transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            isAttacking = false;

            // Verifica si el jugador está activo (por ejemplo, si se está escondiendo podría estar inactivo)
            if (!playerObj.activeInHierarchy)
            {
                Debug.Log("🙈 El murciélago llegó, pero el jugador está escondido. No se aplica daño.");
            }
            else
            {
                Debug.Log("🦇 El murciélago ataca al jugador. Aplicando daño.");
                PlayerHealth ph = playerObj.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(1);  // Ajusta el valor de daño según convenga
                }
                else
                {
                    Debug.LogWarning("No se encontró el componente PlayerHealth en el jugador.");
                }
            }

            StartCoroutine(FlyAway());
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
