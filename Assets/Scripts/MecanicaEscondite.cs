using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour
{
    public float hideDuration = 5f; // Tiempo que el jugador permanece escondido
    private bool isUsed = false;
    private bool playerInZone = false;
    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth; // Referencia al componente de salud del jugador

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

        // Desactivar el controlador del jugador
        if (playerController != null)
            playerController.enabled = false;

        // Marcar al jugador como escondido para evitar la pérdida de vidas
        if (playerHealth != null)
            playerHealth.SetHidden(true);

        // Ocultar al jugador
        player.SetActive(false);

        yield return new WaitForSeconds(hideDuration);

        // Mostrar al jugador y reactivar su controlador
        player.SetActive(true);
        if (playerController != null)
            playerController.enabled = true;

        // Actualizar el estado del jugador a no escondido
        if (playerHealth != null)
            playerHealth.SetHidden(false);

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
            playerHealth = other.GetComponent<PlayerHealth>(); // Obtener la referencia al script de salud
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            player = null;
            playerController = null;
            playerHealth = null;
        }
    }
}

