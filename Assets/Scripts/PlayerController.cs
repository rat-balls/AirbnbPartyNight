using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject playerObj = null;

    [SerializeField] private Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] float walkSpeed = 15.0f;
    [SerializeField] float tiredWalkSpeed = 10.0f;
    private float tempWalkSpeed = 0;

    [SerializeField] float sprintingMultiplier;
    [SerializeField] float crouchingMultiplier;
    [SerializeField] float crouchingSpeed = 0.3f;
    [SerializeField] float gravity = -40f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.0f;

    [SerializeField] bool isSprinting = false;
    [SerializeField] bool isCrouching = false;
    [SerializeField] float crouchHeight = 3.0f;
    [SerializeField] float standingHeight = 8.0f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;


    public float staminaNumber = 0.0f;
    public float staminaMax = 3.0f;
    [SerializeField][Range(0.0f, 100.0f)] float staminaRegenPercent = 1.0f;

    public bool CursorToggled = false;

    public bool isTired = false;
    
    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    void Start()
    {
        
        StartCoroutine("StaminaCheck");

        controller = GetComponent<CharacterController>();

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (playerObj == null)
        {

            playerObj = GameObject.Find("Player");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) {
            Time.timeScale = 10f;
        }

        if(Input.GetKeyDown(KeyCode.L)) {
            Time.timeScale = 1f;
        }

        CursorState();
        isCrouching = Input.GetKey(KeyCode.C);
        
        if (Input.GetKey(KeyCode.LeftShift) && controller.velocity.magnitude >2f && staminaNumber <= staminaMax)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;
        
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        if (isSprinting == true)
        {
            if (isCrouching == false && staminaNumber < staminaMax)
            {
                controller.Move(velocity * sprintingMultiplier * Time.deltaTime);
            }
        }
        if (isCrouching == true)
        {
            controller.Move(velocity * crouchingMultiplier * Time.deltaTime);
        }
        else
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        var desiredHeight = isCrouching ? crouchHeight : standingHeight;

        if (controller.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);

            var camPos = playerCamera.transform.position;
            camPos.y = (controller.height + playerObj.transform.position.y);

            playerCamera.transform.position = camPos;
        }

    }


    private void AdjustHeight(float height)
    {
        float center = height / 2;

        controller.height = Mathf.Lerp(controller.height, height, crouchingSpeed);
        controller.center = Vector3.Lerp(controller.center, new Vector3(0, center, 0), crouchingSpeed);
    }

    public void CursorState()
    {
        if (CursorToggled == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        if (CursorToggled == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator StaminaCheck() {
        while (true){
            yield return new WaitForSeconds (0.1f);
            if (isSprinting == true && staminaNumber < staminaMax){
                staminaNumber += 0.1f;
            }
            if (isSprinting == false && staminaNumber > 0 && staminaNumber < staminaMax){
                staminaNumber -=  staminaMax * staminaRegenPercent / 100;
            }
            if (isSprinting == false && staminaNumber >= staminaMax){
                tempWalkSpeed = walkSpeed;
                walkSpeed = tiredWalkSpeed;
                isTired = true;
                yield return new WaitForSeconds (4.0f);
                staminaNumber = 2.9f;
                walkSpeed = tempWalkSpeed;
                isTired = false;
            }
        }
    }
}