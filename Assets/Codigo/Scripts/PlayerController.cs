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

    private Animator animator;

    [HideInInspector] public bool puedeMover = true;

    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        puedeMover = true;
    }

    void Update()
    {
        if (!puedeMover) return;

        // Captura input
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        if (movePlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movePlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }


        // Movimiento físico
        player.Move(movePlayer * playerSpeed * Time.deltaTime);

        // Animación: cambiar a caminar si hay input
        bool estaCaminando = movePlayer.magnitude > 0.01f;
        animator.SetBool("isWalking", estaCaminando);
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
