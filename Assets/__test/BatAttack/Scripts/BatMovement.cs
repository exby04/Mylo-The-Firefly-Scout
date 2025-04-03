using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float attackDistance = 1f;

    private bool isAttacking = false;
    private Transform player;
    private GameObject playerObj;

    // Modified to accept player reference directly
    public void FlyToAttack(GameObject targetPlayer)
    {
        if (targetPlayer != null)
        {
            playerObj = targetPlayer;
            player = playerObj.transform;
            isAttacking = true;
        }
        else
        {
            Debug.LogWarning("🛑 Player reference is null. Cannot start attack.");
        }
    }

    void Update()
    {
        if (!isAttacking || player == null || playerObj == null) return;

        // Cancel attack if player is hidden
        if (!playerObj.activeInHierarchy)
        {
            isAttacking = false;
            Debug.Log("🙈 Bat arrived, but player is hidden inside an Escondite. No attack!");
            StartCoroutine(FlyAway());
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            isAttacking = false;
            Debug.Log("🦇 Bat attacked the player! (Apply 0.5 damage here)");
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

        gameObject.SetActive(false);
    }
}
