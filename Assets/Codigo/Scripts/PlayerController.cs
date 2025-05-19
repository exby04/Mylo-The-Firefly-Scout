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

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        if (movePlayer.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movePlayer);
            float angle = Quaternion.Angle(transform.rotation, targetRotation);

            if (angle > 0.5f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }




        player.Move(movePlayer * playerSpeed * Time.deltaTime);

        // solo activa caminar si no está corriendo
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = movePlayer.magnitude > 0.01f && !isRunning;
        animator.SetBool("isWalking", isWalking);
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

    public bool IsMoving()
    {
        return movePlayer.magnitude > 0.01f;
    }
}
