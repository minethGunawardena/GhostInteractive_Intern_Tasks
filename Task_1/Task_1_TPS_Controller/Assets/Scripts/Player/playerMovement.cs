using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Locomotion settings")]
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private bool applyGravity = true;

    [Header("Mouse settings")]
    [SerializeField] private float Sencitivity = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;

    [Header("Misc")]
    [SerializeField] private Transform cameraSocket;

    [SerializeField] private LayerMask groundMask;
    private CharacterController characterController;

    

    private bool isGrounded = false;
    private float verticalVelocity;
    private float baseSpeed;
    private float rotY;
    private float rotX;
    private PlayerInputManager playerInputManager;
    private Vector3 playerVelocity;


    [Header("Animation")]
    [SerializeField]private Animator playerAnimator;
    [SerializeField]private float animationBlendSmoothness;
    private float currentX = 0f;
    private float currentY = 0f;
    private int _xVeloHash;
    private int _yVeloHash;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        _xVeloHash = Animator.StringToHash("XVelocity");
        _yVeloHash = Animator.StringToHash("YVelocity");

    }

    private void Update()
    {
       playerVelocity = characterController.velocity;
       ApplyGravity();
       HandleMouseLook();
       HandelMovement();
       handelAnimation();
    }

    private void HandelMovement()
    {
        Vector2 inputAxisVector = playerInputManager.movementInput;

        Vector3 move =transform.right*inputAxisVector.x +transform.forward*inputAxisVector.y;
        baseSpeed = walkingSpeed;
        characterController.Move(move*baseSpeed *Time.deltaTime);
        
    }
    private void HandleMouseLook()
    {
        float mouseRotationX = playerInputManager.lookInput.x;
        float mouseRotationY = -playerInputManager.lookInput.y;

        rotY += mouseRotationX * Sencitivity * Time.deltaTime;
        rotX += mouseRotationY * Sencitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -lookXLimit, lookXLimit);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        cameraSocket.transform.rotation = localRotation;
        transform.rotation = Quaternion.Euler(0, rotY, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool GroundCheck()
    {
        float offset = 0.01f;
        float rayDistance = (characterController.height / 2) + offset;
        RaycastHit hit;

        if (Physics.Raycast(characterController.transform.position+characterController.center,Vector3.down,out hit,rayDistance,groundMask))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded= false;
        }
        return isGrounded;


    }

    private void ApplyGravity()
    {
        if (applyGravity)
        {
            if (!GroundCheck())
            {
                verticalVelocity -= gravity*Time.deltaTime;
            }
            else
            {
                verticalVelocity = -1;
            }

        }
        Vector3 gravitatiaonalAccelaration = new Vector3(0, verticalVelocity, 0);
        characterController.Move(gravitatiaonalAccelaration * Time.deltaTime*0.1f);
    }

    private void handelAnimation()
    {


        Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);

        float targetX = Mathf.Clamp(localVelocity.x / walkingSpeed, -1f, 1f);
        float targetY = Mathf.Clamp(localVelocity.z / walkingSpeed, -1f, 1f);

        currentX = Mathf.Lerp(currentX, targetX,animationBlendSmoothness*Time.deltaTime*2);
        currentY = Mathf.Lerp(currentY, targetY,animationBlendSmoothness*Time.deltaTime*2);

        playerAnimator.SetFloat(_xVeloHash,currentX);
        playerAnimator.SetFloat(_yVeloHash, currentY);
    }

    
}
