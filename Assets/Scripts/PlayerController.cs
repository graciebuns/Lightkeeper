using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed of the player
    public float gravity = -9.81f; // Gravity force applied to the player
    public float jumpHeight = 2f; // Height the player can jump
    private Transform cameraTransform; // Reference to the main camera (used for movement direction)

    private CharacterController controller; // CharacterController component for handling collisions and movement
    private Vector3 velocity; // Stores vertical velocity for jumping and gravity
    private bool isGrounded; // Checks if the player is on the ground
    private float turnSmoothVelocity; // Used to smooth the turning of the player

    private int jumps = 2;

    void Awake()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    void Start()
    {
        cameraTransform = Camera.main.transform; // Store reference to the main camera's transform
    }

    void Update()
    {
        // Check if the player is touching the ground
        isGrounded = controller.isGrounded;

        // // Get input axes for movement
        // float moveX = Input.GetAxis("Horizontal");
        // float moveZ = Input.GetAxis("Vertical");

        // // Create a normalized direction vector from input
        // Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        // // Only process movement and rotation if there's input
        // if (inputDir.magnitude >= 0.1f)
        // {
        //     // Calculate the direction relative to the camera's current Y rotation
        //     float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        //     // Smooth the rotation to avoid instant snapping
        //     float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);

        //     // Rotate the player toward the target angle
        //     transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

        //     // Calculate movement direction based on the target angle
        //     Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        //     // Move the player using the CharacterController
        //     controller.Move(moveDir.normalized * speed * Time.deltaTime);
        // }

        // If grounded and falling down, apply a small downward force
        // This keeps the player "snapped" to the ground instead of slightly floating
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Set to a small negative value to maintain contact with ground
        }

        // If the jump button is pressed and the player is on the ground, calculate jump velocity
        if (jumps > 0 && Input.GetButtonDown("Jump"))
        {
            // Use physics formula to calculate upward velocity for desired jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumps--; // para dli sigeg saka ang player :)
        }

        // Apply gravity to the vertical velocity
        velocity.y += gravity * Time.deltaTime;

        // Apply the vertical movement (from gravity and jumping)
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded)
        {
            jumps = 2;
        }

        private void CalculateMoveRot()
        {
                // Get input axes for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a normalized direction vector from input
        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        // Only process movement and rotation if there's input
        if (inputDir.magnitude >= 0.1f)
        {
            // Calculate the direction relative to the camera's current Y rotation
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Smooth the rotation to avoid instant snapping
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);

            // Rotate the player toward the target angle
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            // Calculate movement direction based on the target angle
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the player using the CharacterController
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        }
    }
}