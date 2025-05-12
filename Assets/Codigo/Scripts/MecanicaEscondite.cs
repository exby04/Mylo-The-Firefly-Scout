using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour
{
    public float hideDuration = 5f;
    private bool isUsed = false;
    private bool playerInZone = false;
    private bool isHiding = false;
    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    public GameObject usedAssetPrefab;
    public Light troncoLight;
    public GameObject hideLabelUI; // "Esconderse [E]"
    public GameObject exitLabelUI; // "Salir [E]"
    
    private Coroutine hidingCoroutine;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (troncoLight != null)
            troncoLight.enabled = false;

        if (hideLabelUI != null)
            hideLabelUI.SetActive(false);

        if (exitLabelUI != null)
            exitLabelUI.SetActive(false);
    }

    void Update()
    {
        if (playerInZone && !isUsed && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHiding)
            {
                // Start hiding
                hidingCoroutine = StartCoroutine(HideRoutine());
            }
            else
            {
                // Manual exit
                StopCoroutine(hidingCoroutine);
                ExitHiding();
            }
        }
    }

    private IEnumerator HideRoutine()
    {
        isHiding = true;

        // Swap labels
        if (hideLabelUI != null)
            hideLabelUI.SetActive(false);
        if (exitLabelUI != null)
            exitLabelUI.SetActive(true);

        if (troncoLight != null)
            troncoLight.enabled = true;

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
        isUsed = true;

        if (troncoLight != null)
            troncoLight.enabled = false;

        player.SetActive(true);

        if (playerController != null)
            playerController.enabled = true;

        if (playerHealth != null)
            playerHealth.SetHidden(false);

        if (hideLabelUI != null)
            hideLabelUI.SetActive(false);
        if (exitLabelUI != null)
            exitLabelUI.SetActive(false);

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

            if (!isUsed)
            {
                if (!isHiding && hideLabelUI != null)
                    hideLabelUI.SetActive(true);

                if (isHiding && exitLabelUI != null)
                    exitLabelUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInZone = false;

            if (hideLabelUI != null)
                hideLabelUI.SetActive(false);
            if (exitLabelUI != null)
                exitLabelUI.SetActive(false);
        }
    }
}
