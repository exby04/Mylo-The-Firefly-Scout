using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    public float hideDuration = 5f;
    public float holdTimeToHide = 2f;

    [Header("Prefab visual del escondite usado")]
    public GameObject usedAssetPrefab;

    private bool isUsed = false;
    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    // ✅ Implementación de la interfaz
    public float HoldDuration => holdTimeToHide;

    public void OnInteract()
    {
        if (isUsed || player == null) return;
        StartCoroutine(HideRoutine());
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
}
