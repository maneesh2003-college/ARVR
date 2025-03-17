using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;
    public float jumpForce = 5f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20f;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            // Get input for movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Calculate movement direction
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            // Jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }

            // Apply animations if an animator is present
            if (animator != null)
            {
                animator.SetFloat("Speed", (horizontal * horizontal + vertical * vertical));
                animator.SetBool("IsGrounded", isGrounded);
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        // Rotate the player based on horizontal input
        float rotationAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Collectible"))
        {
            GameManager.instance.AddScore(10);
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.CompleteLevel();
        }
        else if (hit.gameObject.CompareTag("Hazard"))
        {
            GameManager.instance.LoseChance();
        }
    }
}