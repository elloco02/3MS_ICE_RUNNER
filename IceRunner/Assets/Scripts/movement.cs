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
    [SerializeField] private float gravity = -9.81f; 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
     [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 2f;

    private Vector3 velocity;
     private bool isGrounded;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        playerController = GetComponent<CharacterController>();
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
        // check if player is on ground (no moving while jumping)
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    	if(playerController.isGrounded){

            // Ground contact logic
            if (velocity.y < 0)
            {
                velocity.y = -2f; // player is forced to ground
            }

            moveDirection = move.ReadValue<Vector2>();
            movePlayer = this.transform.right * moveDirection.x + this.transform.forward;
            playerController.Move(movePlayer * moveSpeed * Time.deltaTime);

            // Jump logic
            if (jump.triggered && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // apply gravity
            velocity.y += gravity * Time.deltaTime;
            playerController.Move(velocity * Time.deltaTime);
        }
    }
}
