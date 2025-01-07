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
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravityFactor = 2f;
    [SerializeField] private float smoothingFactor = 5f; // Wie schnell die Bewegung reagiert

    private float smoothedMoveX = 0f;
    private float moveSpeed;
    private Vector3 velocity;
    private bool canJump = true;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        playerController = GetComponent<CharacterController>();
        moveSpeed = GameManager.Instance.currentMoveSpeed;
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

        // Glättung der horizontalen Bewegung
        smoothedMoveX = Mathf.Lerp(smoothedMoveX, moveDirection.x, Time.deltaTime * smoothingFactor);

        // Bewegungsrichtung mit geglätteter horizontaler Eingabe
        movePlayer = this.transform.right * smoothedMoveX + this.transform.forward;
        
        playerController.Move(movePlayer * moveSpeed * Time.deltaTime);

        // Jump logic
        if (jump.triggered && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = false;
        }

        // apply gravity
        velocity.y += gravity * Time.deltaTime * gravityFactor;
        playerController.Move(velocity * Time.deltaTime);
    }
}
