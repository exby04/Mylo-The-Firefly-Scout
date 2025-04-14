using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude > 1f)
            direction = direction.normalized;

        controller.Move(direction * speed * Time.deltaTime);

        animator.SetFloat("Speed", direction.magnitude);
    }
}
