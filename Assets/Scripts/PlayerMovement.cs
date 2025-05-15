using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float gravity = -9.81f;
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Input
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        // Move direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Update Animator
        animator.SetBool("forward",(Input.GetKey(KeyCode.W)));
        animator.SetBool("backward",(Input.GetKey(KeyCode.S)));
        animator.SetBool("right",(Input.GetKey(KeyCode.D)));
        animator.SetBool("left",(Input.GetKey(KeyCode.A)));
    }
}
