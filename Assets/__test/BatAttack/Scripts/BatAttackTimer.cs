using System.Collections;
using UnityEngine;

public class BatAttackTimer : MonoBehaviour
{
    [Header("Bat Attack Settings")]
    public float attackInterval = 40f;
    private float timer = 0f;
    private bool hasShownWarning = false;

    [Header("References")]
    public GameObject batPrefab;
    public Transform[] batSpawnPoints;
    public GameObject warningText;

    private GameObject cachedPlayer;

    void Start()
    {
        cachedPlayer = GameObject.FindWithTag("Player");

        if (cachedPlayer == null)
            Debug.LogWarning("🛑 Cached player not found at Start(). Make sure the player is tagged correctly.");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!hasShownWarning && timer >= attackInterval - 5f)
        {
            ShowWarning();
        }

        if (timer >= attackInterval)
        {
            StartCoroutine(TriggerBatAttack());
            timer = 0f;
            hasShownWarning = false;
        }
    }

    void ShowWarning()
    {
        if (warningText != null)
        {
            warningText.SetActive(true);
            Debug.Log("⚠️ ¡Murciélagos en camino!");
        }

        hasShownWarning = true;
    }

    IEnumerator TriggerBatAttack()
    {
        if (warningText != null)
            warningText.SetActive(false);

        Debug.Log("🦇 ¡Ataque de murciélagos!");

        if (batSpawnPoints.Length > 0 && batPrefab != null && cachedPlayer != null)
        {
            GameObject bat = Instantiate(batPrefab, batSpawnPoints[0].position, Quaternion.identity);
            BatMovement batScript = bat.GetComponent<BatMovement>();

            if (batScript != null)
            {
                batScript.FlyToAttack(cachedPlayer); // Pass player reference here
            }
            else
            {
                Debug.LogWarning("🛑 Bat prefab is missing BatMovement script.");
            }
        }
        else
        {
            Debug.LogWarning("🛑 Missing bat prefab, spawn point, or cached player.");
        }

        yield return null;
    }
}
