using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private CharacterController playerController;
    private InputSystem_Actions inputActions;
    private InputAction move;
    private InputAction jump;

    Vector2 moveDirection = Vector2.zero;
    Vector3 movePlayer = Vector3.zero;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float speedIncreaseRate = 0.1f; // acceleration as time flies
    [SerializeField] private int maxMoveSpeed = 50;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private float currentMoveSpeed;
    private Vector3 velocity;
    private bool canJump = true;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        playerController = GetComponent<CharacterController>();
        currentMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        jump = inputActions.Player.Jump;
        move.Enable();
        jump.Enable();

    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveSpeed += speedIncreaseRate * Time.deltaTime;
        currentMoveSpeed = Mathf.Min(currentMoveSpeed, maxMoveSpeed);
        if (playerController.isGrounded)
        {
            velocity.y = -2f; // player is forced to ground
            canJump = true;  // allows player to jump from ground
        }
        else
        {
            canJump = false; // denies jumping in the air
        }

        moveDirection = move.ReadValue<Vector2>();
        movePlayer = this.transform.right * moveDirection.x + this.transform.forward;
        playerController.Move(movePlayer * currentMoveSpeed * Time.deltaTime);

        // Jump logic
        if (jump.triggered && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = false;
        }

        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }
}
