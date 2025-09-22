using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public int playerDamage;
    public Animator playerAnimator;
    public List<GameObject> enemyList = new List<GameObject>();

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private float turnSmoothVelocity;
    private int jumps = 2;
    private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Use raycast to check if grounded instead of controller.isGrounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f + 0.1f);

        CalculateMoveRot();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (jumps > 0 && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumps--;
        }

        if (isGrounded && Input.GetMouseButtonDown(0)) // Left click - Attack
        {
            Attack();
        }

        if (isGrounded && Input.GetMouseButtonDown(1)) // Right click - Heavy Attack
        {
            HandleAnimation("Kick");
        }

        if (Input.GetKeyDown(KeyCode.E)) // Press E to Pick
        {
            HandleAnimation("Pick");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded)
        {
            jumps = 2;
            playerAnimator.SetBool("Grounded", true);
        }
        else
        {
            playerAnimator.SetBool("Grounded", false);
        }
    }

    void CalculateMoveRot()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            playerAnimator.SetBool("isWalking", true);

            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
        HandleAnimation("Attack");

        foreach (var enemy in enemyList)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(playerDamage);
                Debug.Log(damageable.GetHealth());
            }
        }
    }

    void HandleAnimation(string animationString)
    {
        switch (animationString)
        {
            case "Run":
                if (isGrounded)
                {
                    playerAnimator.SetBool("isMoving", true);
                    playerAnimator.SetBool("isJumping", false);
                }
                else
                {
                    playerAnimator.SetBool("isMoving", false);
                }
                break;

            case "Jump":
                playerAnimator.SetBool("isMoving", false);
                playerAnimator.SetBool("isJumping", true);
                break;

            case "Attack":
                playerAnimator.SetBool("isMoving", false);
                playerAnimator.SetBool("isJumping", false);
                playerAnimator.SetTrigger("isAttacking");
                break;

            case "Pick":
                playerAnimator.SetTrigger("isPicking");
                break;

            case "Kick":
                playerAnimator.SetTrigger("isKick");
                break;

            default:
                break;
        }
    }
}
