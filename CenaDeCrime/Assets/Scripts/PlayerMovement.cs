using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
enum CrouchState
{
    standing,
    crouching,
    laying
}
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoviment : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] Transform playerCamera;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float aceleration = 2f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector2 lookInput;
    [Space]
    [SerializeField] private Transform orientation;
    [Space]
    [SerializeField] private float jumpForce = 5f;
    public enum MovementState { walking, sprinting }
    public MovementState moveState;

    [Header("Public Settings")]
    public bool canMoveCam = true;
    public bool canMove = true;

    [Header("Camera Settings")]
    [SerializeField] private float camLerpSmooth = 10f;
    [SerializeField] private float mouseSensitivity = 25;
    [SerializeField] private float xRotation = 0f;
    [SerializeField] private float yRotation = 0f;

    [Header("SprintConfig")]
    [SerializeField] bool isSprinting;
    [SerializeField] bool canSprint;
    [SerializeField] float sprintAceleration = 5f;
    [SerializeField] float sprintMaxSpeed = 20f;

    [Header("Stamina")]
    [SerializeField] float currentStamina;
    [SerializeField] float maxStamina;
    [SerializeField] float timeToBeginRecoveringStamina = 1f;
    [SerializeField] float timeToRecoverStamina = 2f;
    [SerializeField] bool isRecoveringStamina;

    [Header("Debug")]
    [SerializeField] Vector3 currentVelocity;
    [SerializeField] bool isGrounded;
    [SerializeField] bool startSliding;

    Rigidbody rb;
    private void Awake()
    {
        GameManager.instance.playerMovement = this;
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();

        currentStamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void FixedUpdate()
    {
        if (moveInput != Vector3.zero)
        {
            speed += isSprinting ? sprintAceleration : aceleration;
            speed = Mathf.Clamp(speed, 0, isSprinting ? sprintMaxSpeed : maxSpeed);
            if (moveInput.magnitude > 1) moveInput = moveInput.normalized;
        }
        else
            speed = Mathf.MoveTowards(speed, 0, deceleration);

        StaminaControl();
        MovePlayer();
    }
    #region Stamina
    void StaminaControl()
    {
        if (!isSprinting) return;
        if (isRecoveringStamina) return;

        currentStamina -= Time.deltaTime;
        if (currentStamina <= 0)
        {
            canSprint = false;
            isSprinting = false;
            StartCoroutine(StaminaRecoverCooldown());
        }
    }
    IEnumerator StaminaRecoverCooldown()
    {
        isRecoveringStamina = true;
        float t = 0;
        yield return new WaitForSeconds(timeToBeginRecoveringStamina);
        while (t < timeToRecoverStamina)
        {
            if (!isRecoveringStamina) yield break;
            t += Time.deltaTime;
            currentStamina = Mathf.Lerp(0, maxStamina, t / timeToRecoverStamina);
            yield return null;
        }
        currentStamina = maxStamina;
        isRecoveringStamina = false;
        canSprint = true;
    }
    void StopRecoveringStamina()
    {
        StopCoroutine("StaminaRecoverCooldown");
        isRecoveringStamina = false;
        canSprint = true;
        isSprinting = true;
    }
    #endregion
    void MovePlayer()
    {
        if (!canMove) return;

        Vector3 movementDir = orientation.forward * moveInput.z + orientation.right * moveInput.x;
        movementDir.y = 0;

        rb.linearVelocity = new Vector3(movementDir.x * speed, rb.linearVelocity.y, movementDir.z * speed);

#if UNITY_EDITOR
        currentVelocity = rb.linearVelocity;
#endif
    }
    private void Update()
    {
        if (Time.timeScale == 0 || !canMoveCam) return;

        MoveCam();
    }
    void MoveCam()
    {
        if(!canMoveCam) return;
        float camInputY;
        float camInputX;

        camInputY = lookInput.y * mouseSensitivity;
        camInputX = lookInput.x * mouseSensitivity;

        yRotation += camInputX;
        xRotation -= camInputY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        xRotation = Mathf.Lerp(xRotation, xRotation, Time.deltaTime * camLerpSmooth);
        yRotation = Mathf.Lerp(yRotation, yRotation, Time.deltaTime * camLerpSmooth);

        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    #region Inputs
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector3>();
            moveState = isSprinting ? MovementState.sprinting : MovementState.walking;
        }
        else if (context.canceled)
            moveInput = Vector3.zero;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed) lookInput = context.ReadValue<Vector2>();
        else if (context.canceled) lookInput = Vector2.zero;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canSprint)
                isSprinting = true;
            else
                StopRecoveringStamina();
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }
    #endregion
    #region Setvariables
    public void SetSensibility(float value)
    {
        mouseSensitivity = value;
    }
    #endregion
    #region GetValues
    public bool isMoving() => moveInput.magnitude > 0;
    #endregion
}
