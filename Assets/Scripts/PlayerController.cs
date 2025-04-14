using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;
    private Vector3 playerInput;

    public float playerSpeed;
    private Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public Animator animator; // <- Nuevo

    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // <- Asegura que se agarre del hijo si está en el modelo
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        if (movePlayer != Vector3.zero)
            transform.LookAt(transform.position + movePlayer);

        player.Move(movePlayer * playerSpeed * Time.deltaTime);

        // Animación
        float speedPercent = playerInput.magnitude;
        if (animator != null)
            animator.SetFloat("Speed", speedPercent);
        else
            Debug.LogWarning("Animator no asignado.");
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
}
