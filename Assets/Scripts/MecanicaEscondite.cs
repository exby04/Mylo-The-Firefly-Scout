using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour
{
    public float hideDuration = 5f; // Tiempo que el jugador permanece escondido
    private bool isUsed = false;
    private bool playerInZone = false;
    private GameObject player;
    private PlayerController playerController;

    // Prefab del asset que representa el estado "usado"
    public GameObject usedAssetPrefab;

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

        // Oculta al jugador
        player.SetActive(false);

        yield return new WaitForSeconds(hideDuration);

        // Reaparece al jugador y reactiva su controlador
        player.SetActive(true);
        if (playerController != null)
            playerController.enabled = true;

        // Instanciar el prefab del asset "usado" en la posición y rotación actuales
        if (usedAssetPrefab != null)
        {
            Instantiate(usedAssetPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            player = other.gameObject;
            playerController = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            player = null;
            playerController = null;
        }
    }
}

