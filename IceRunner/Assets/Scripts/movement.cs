using System.Collections;
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
    [SerializeField] private float speedBoostMultiplier = 2.5f;
    [SerializeField] private float speedBoostDuration = 4f;


    private float smoothedMoveX = 0f;
    private float moveSpeed;
    private Vector3 velocity;
    private bool canJump = true;
    private bool isBoostActive = false;
    private float boostEndTime;

    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;
    
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

            // Reset speed boost when grounded
            if (isBoostActive)
            {
                moveSpeed = GameManager.Instance.currentMoveSpeed;
                isBoostActive = false;
            }
        }
        else
        {
            canJump = false; // denies jumping in the air
        }

        if (jump.triggered)
        {
            if (canJump) // Jump from the ground
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canJump = false;
                hasDoubleJumped = false; // Reset double jump usage
                
                ActivateSpeedBoost();
            }
            else if (canDoubleJump && !hasDoubleJumped) // Double jump logic
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                hasDoubleJumped = true; // Prevent further double jumps
            }
        }
        
        moveDirection = move.ReadValue<Vector2>();

        // Glättung der horizontalen Bewegung
        smoothedMoveX = Mathf.Lerp(smoothedMoveX, moveDirection.x, Time.deltaTime * smoothingFactor);

        // Bewegungsrichtung mit geglätteter horizontaler Eingabe
        movePlayer = this.transform.right * smoothedMoveX + this.transform.forward;

        playerController.Move(movePlayer * moveSpeed * Time.deltaTime);
        
        // apply gravity
        velocity.y += gravity * Time.deltaTime * gravityFactor;
        playerController.Move(velocity * Time.deltaTime);

        // Deactivate speed boost after duration
        if (isBoostActive && Time.time > boostEndTime)
        {
            moveSpeed = GameManager.Instance.currentMoveSpeed;
            isBoostActive = false;
        }
    }

    private void ActivateSpeedBoost()
    {
        moveSpeed *= speedBoostMultiplier;
        isBoostActive = true;
        boostEndTime = Time.time + speedBoostDuration;
    }

    public void BoostSpeed(float speedMultiplier, float duration)
    {
        StartCoroutine(BoostSpeedCoroutine(speedMultiplier, duration));
    }

    private IEnumerator BoostSpeedCoroutine(float speedMultiplier, float duration)
    {
        float originalSpeed = moveSpeed; 
        moveSpeed *= speedMultiplier;   

        yield return new WaitForSeconds(duration); 

        moveSpeed = originalSpeed;      
    }

    public void EnableDoubleJump(float duration)
    {
        StartCoroutine(DoubleJumpCoroutine(duration));
    }
    
    private IEnumerator DoubleJumpCoroutine(float duration)
    {
        canDoubleJump = true;

        yield return new WaitForSeconds(duration); 
        
        canDoubleJump = false;
    }
}
