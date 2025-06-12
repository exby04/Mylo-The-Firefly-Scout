using UnityEngine;

public class SpeedPowerUpController : MonoBehaviour
{
    public PlayerController playerController;
    public float speedMultiplier = 2f;
    public float powerUpDuration = 10f;

    public Animator animator;

    private bool isActive = false;
    private float timer = 0f;
    private float originalSpeed;

    private MyloAudio myloAudio;


    void Start()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        if (animator == null)
            animator = GetComponent<Animator>();

        originalSpeed = playerController.playerSpeed;
        myloAudio = GetComponent<MyloAudio>();

    }

    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndPowerUp();
            }
            else
            {
                UpdateRunningState();
            }
        }
    }

    public void GiveSpeedPowerUp()
    {
        ActivatePowerUp();
        Debug.Log(" PowerUp de velocidad recibido.");
    }

    private void ActivatePowerUp()
    {
        isActive = true;
        timer = powerUpDuration;
        playerController.playerSpeed = originalSpeed * speedMultiplier;

        UpdateRunningState();
        Debug.Log(" PowerUp activado");
    }

    private void EndPowerUp()
    {
        isActive = false;
        playerController.playerSpeed = originalSpeed;
        animator.SetBool("isRunning", false);
        if (myloAudio != null)
        {
            myloAudio.SonidoCansancioConDuracion(3f); 
        }
        Debug.Log(" PowerUp finalizado");
    }

    private void UpdateRunningState()
    {
        bool isMoving = playerController.IsMoving();
        animator.SetBool("isRunning", isMoving);
    }
}
