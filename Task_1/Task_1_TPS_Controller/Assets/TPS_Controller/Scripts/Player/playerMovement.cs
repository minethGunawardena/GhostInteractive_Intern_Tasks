using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private bool applyGravity = true;

    [SerializeField] private float Sencitivity = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;

    [SerializeField] private Transform cameraSocket;

    [SerializeField] private LayerMask groundMask;
    private CharacterController characterController;



    private bool isGrounded = false;

    private float verticalVelocity;
    private float baseSpeed;
    private float rotY;
    private float rotX;


    [SerializeField]private Animator playerAnimator;
    [SerializeField]private float animationBlendSpeed;
    private int _xVeloHash;
    private int _yVeloHash;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        _xVeloHash = Animator.StringToHash("Xvelocity");
        _yVeloHash = Animator.StringToHash("Yvelocity");

    }

    private void Update()
    {
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
                verticalVelocity -=gravity*Time.deltaTime;
            }
            else
            {
                verticalVelocity = -1;
            }

        }
        Vector3 gravitatiaonalAccelaration = new Vector3(0, verticalVelocity, 0);
        characterController.Move(gravitatiaonalAccelaration * Time.deltaTime);
    }

    private void handelAnimation()
    {

        playerAnimator.SetFloat(_xVeloHash,playerInputManager.movementInput.x*2f);
        playerAnimator.SetFloat(_yVeloHash, playerInputManager.movementInput.y*2f);
    }

    
}
