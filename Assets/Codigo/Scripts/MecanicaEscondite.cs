using UnityEngine;

public class HideSpot : MonoBehaviour
{
    private bool isUsed = false;
    private bool playerInZone = false;
    private bool isHiding = false;

    private GameObject player;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private AtenuacionLuz atenuacionLuz;
    private CharacterController characterController;

    public GameObject usedAssetPrefab;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
            atenuacionLuz = player.GetComponentInChildren<AtenuacionLuz>();
            characterController = player.GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        if (playerInZone && (isHiding || !isUsed) && Input.GetKeyDown(KeyCode.E))

        {
            if (!isHiding)
                EnterHide();
            else
                ExitHide();
        }
    }

    private void EnterHide()
    {
        isHiding = true;
        isUsed = true;

        if (playerController != null)
        {
            playerController.puedeMover = false;
            playerController.horizontalMove = 0;
            playerController.verticalMove = 0;
        }

        if (playerHealth != null)
            playerHealth.SetHidden(true);


        // Hide only visuals, keep LuzJugador lights visible
        foreach (Transform child in player.transform)
        {
            if (!child.name.StartsWith("LuzJugador"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void ExitHide()
    {
        isHiding = false;

        if (playerController != null)
            playerController.puedeMover = true;

        if (playerHealth != null)
            playerHealth.SetHidden(false);

        if (characterController != null)
            characterController.enabled = true;

        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(true);
        }

        if (usedAssetPrefab != null)
        {
            Instantiate(usedAssetPrefab, transform.position, transform.rotation);
        }

        Transform simbolo = player.transform.Find("SimboloExclamacion");
      if (simbolo != null)
     {
      simbolo.gameObject.SetActive(false);
     }


        Destroy(gameObject);
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


