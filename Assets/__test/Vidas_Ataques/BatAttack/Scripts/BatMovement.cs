using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float speed = 5f;
    public float attackDistance = 1f;

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
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
                // Obtén y llama al componente PlayerHealth para aplicar daño
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
            transform.position = Vector3.MoveTowards(transform.position, exitPoint, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
