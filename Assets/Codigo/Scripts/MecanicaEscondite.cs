using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour
{
    public float hideDuration = 5f;
    private bool isUsed = false;
    private bool playerInZone = false;
    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    public GameObject usedAssetPrefab;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (playerInZone && !isUsed && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(HideRoutine());
        }
    }

    private IEnumerator HideRoutine()
    {
        isUsed = true;

        if (playerController != null)
            playerController.enabled = false;

        if (playerHealth != null)
            playerHealth.SetHidden(true);

        player.SetActive(false);

        yield return new WaitForSeconds(hideDuration);

        player.SetActive(true);
        if (playerController != null)
            playerController.enabled = true;

        if (playerHealth != null)
            playerHealth.SetHidden(false);

        if (usedAssetPrefab != null)
        {
            Instantiate(usedAssetPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInZone = false;
        }
    }
}
