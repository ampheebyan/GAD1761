using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    // Public API variables.
    [Header("API Variables")]
    public bool isGrounded = false;

    public bool isCrouching = false;

    // 0 = just moving, 1 = is the player running?
    public bool[] isMoving = new bool[] {false, false};
    
    // Visible in the inspector.
    [Header("Properties")]
    [SerializeField]
    private Camera playerCamera;

    [Header("Movement Properties")]
    [SerializeField]
    private float walkingSpeed = 5f;

    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private float playerGravity = 20f;

    [SerializeField]
    private float playerJump = 1f;

    [SerializeField]
    private float crouchHeight = 1f;

    [SerializeField]
    private float dashForce = 12f;

    [SerializeField]
    private float dashDelay = 0.25f;

    [SerializeField]
    private int maxDashes = 2;

    [SerializeField]
    private Vector2 sensitivity = new Vector2 {
        x = 2f,
        y = 2f
    };

    [Header("Input")]
    public KeyCode crouchKey = KeyCode.C;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode dashKey = KeyCode.LeftShift;

    // Completely non-public variables.
    private CharacterController characterController;
    [SerializeField]
    private MovementZoneInfo currentMovementZone;

    private float defaultHeight;
    private Vector3 defaultCamPos;

    private ExtPlayer extendedPlayer;

    private Vector3 startingTransform;
    private Quaternion startingRotation;

    private Vector2 rot;
    private Vector3 playerVelocity;

    private int dashCount = 0;
    private bool cursorLocked = false;
    public void Awake() 
    {
        if(!TryGetComponent<CharacterController>(out characterController)) {
            throw new System.Exception("No character controller on Player object!");
        } else {
            defaultHeight = characterController.height;
        }

        if(!TryGetComponent<ExtPlayer>(out extendedPlayer)) {
            throw new System.Exception("No ExtPlayer on Player object!");
        }

        defaultCamPos = playerCamera.transform.localPosition;
        startingTransform = transform.position;
        startingRotation = transform.rotation;
    }
    public void ResetPos()
    {
        transform.position = startingTransform;
        transform.rotation = startingRotation;
    }
    
    public void MouseLockHandler() {
        if(cursorLocked) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetMouseButtonDown(0) && cursorLocked == false) {
            cursorLocked = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && cursorLocked == true) {
            cursorLocked = false;
        }
    }

    public void MouseLook(Vector2 mouse) 
    {
        rot.x -= mouse.y * sensitivity.y;
        rot.y -= mouse.x * sensitivity.x;

        rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        this.transform.eulerAngles = new Vector3(0, rot.y * -1, 0);

        playerCamera.transform.localEulerAngles = new Vector3(rot.x, 0, 0);
    }

    IEnumerator DashCoroutine(Vector3 movement)
    {
        float internalDashStartTime = Time.time;

        while(Time.time < internalDashStartTime + dashDelay)
        {
            characterController.Move(movement * dashForce * Time.deltaTime);
            yield return null;
        }
    }

    public void CharacterMove(Vector2 movement) 
    {
        if(Input.GetKey(crouchKey)) {
            isCrouching = true;
            playerCamera.transform.localPosition = defaultCamPos - new Vector3(0, (defaultHeight - crouchHeight) / 2, 0);
            characterController.height = crouchHeight;
        } else {
            isCrouching = false;
            characterController.height = defaultHeight;
            playerCamera.transform.localPosition = defaultCamPos;
        }

        if (Input.GetKey(jumpKey))
        {
            if(isGrounded)
                playerVelocity.y = Mathf.Sqrt(playerJump * -2 * -playerGravity);
        }

        Vector3 movementVector = transform.right * movement.x + transform.forward * movement.y;

        if (isGrounded)
        {
            if(playerVelocity.y < 0) playerVelocity.y = -2;
            dashCount = 0;
        } else
        {
            if(Input.GetKeyDown(dashKey))
            {
                if(dashCount != maxDashes)
                {
                    dashCount++;
                    StartCoroutine(DashCoroutine(movementVector));
                }
            }
        }

        playerVelocity.y += -playerGravity * Time.deltaTime;

        isMoving[1] = extendedPlayer.stamina.x <= 0 ? false : Input.GetKey(sprintKey);
        float movementSpeed = isMoving[1] ? runSpeed : walkingSpeed;

        if(currentMovementZone != null) {
            movementSpeed += currentMovementZone.speedModifier;
        }

        Vector3 motion = movementVector * movementSpeed * Time.deltaTime + playerVelocity * Time.deltaTime;

        isMoving[0] = motion.x != 0 ||motion.z != 0;

        Physics.SyncTransforms();
        characterController.Move(motion);
    }

    public void MovementZoneHandler() 
    {
        if(Physics.Raycast(transform.position, new Vector3(0,-1,0), out RaycastHit hit,2f)) {
            if(hit.collider.gameObject.TryGetComponent<MovementZoneInfo>(out MovementZoneInfo movementZoneInfo)) {
                currentMovementZone = movementZoneInfo;
            } else {
                currentMovementZone = null;
            }
        }
    }

    public void Update()
    {
        Vector2 mouse = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

        Vector2 movement = new Vector2 {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };

        isGrounded = characterController.isGrounded;

        MouseLockHandler();
        MouseLook(mouse);

        MovementZoneHandler();
        CharacterMove(movement);
    }
}
