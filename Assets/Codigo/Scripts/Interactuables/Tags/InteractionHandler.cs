using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    [Header("Configuración de interacción")]
    public KeyCode interactionKey = KeyCode.E;
    public Transform player;
    public float mostrarUIRadius = 3f;
    public float ocultarUIRadius = 3.5f;

    [Header("UI")]
    public GameObject uiCanvas;
    public Image progressCircle;

    private IInteractable interactable;
    private float holdTimer = 0f;
    private bool playerInRange = false;

    private void Start()
    {
        interactable = GetComponent<IInteractable>();

        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false);
            if (progressCircle != null)
                progressCircle.fillAmount = 0f;
        }

        if (player == null)
        {
            GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
            if (jugadorGO != null)
                player = jugadorGO.transform;
        }
    }

    private void Update()
    {
        if (player == null || interactable == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        //Evita titileo del Canvas
        if (!playerInRange && distance <= mostrarUIRadius)
            playerInRange = true;
        else if (playerInRange && distance > ocultarUIRadius)
            playerInRange = false;

        bool mostrarUI = playerInRange;

        if (interactable is HideSpot hs)
        {
            if (!hs.EstaDisponibleParaInteractuar())
                mostrarUI = false;
        }

        if (uiCanvas != null)
            uiCanvas.SetActive(mostrarUI);


        // Mantener tecla para interactuar
        if (playerInRange && Input.GetKey(interactionKey))
        {
            holdTimer += Time.deltaTime;
            float progress = holdTimer / interactable.HoldDuration;

            if (progressCircle != null)
                progressCircle.fillAmount = Mathf.Clamp01(progress);

            if (holdTimer >= interactable.HoldDuration)
            {
                interactable.OnInteract();
                holdTimer = 0f;

                if (uiCanvas != null)
                {
                    uiCanvas.SetActive(false);
                    if (progressCircle != null)
                        progressCircle.fillAmount = 0f;
                }

                //if (!(interactable is FireflyCollector))
                // {
                //    enabled = false;
                //}
            }
        }
        else if (!Input.GetKey(interactionKey))
        {
            holdTimer = 0f;
            if (progressCircle != null)
                progressCircle.fillAmount = 0f;
        }

        //Rotar la UI hacia la cámara
        if (uiCanvas != null && Camera.main != null)
        {
            uiCanvas.transform.LookAt(Camera.main.transform);
            uiCanvas.transform.Rotate(0, 180f, 0);
        }
    }
}
