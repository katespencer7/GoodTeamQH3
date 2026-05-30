using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Camera")]
    [SerializeField] private Camera isoCamera;

    private Rigidbody rb;
    private PlayerInputActions input;

    private Vector3 currentLookDirection = Vector3.forward;
    private Vector2 moveInput;

    // Tracks which device was used most recently.
    private bool usingGamepad = false;

    private const float GamepadDeadzone = 0.25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerInputActions();

        //rb.freezeRotation = true;

        if (isoCamera == null)
            isoCamera = Camera.main;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Dash.performed += ctx => HandleDash();
    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Dash.performed -= ctx => HandleDash();
    }

    private void Update()
    {
        // Read input in Update for responsiveness.
        moveInput = input.Player.Movement.ReadValue<Vector2>();
        DetectActiveDevice();
        UpdateLookDirection();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Quaternion camYaw = GetCameraYaw();
        Vector3 moveDirection = camYaw * new Vector3(moveInput.x, 0f, moveInput.y);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        Vector3 targetVelocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
        rb.MoveRotation(Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        ));
    }

    private void UpdateLookDirection()
    {
        if (usingGamepad)
            UpdateLookFromStick(GetCameraYaw());
        else
            UpdateLookFromMouse();
    }

    private void UpdateLookFromStick(Quaternion camYaw)
    {
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>();
        if (lookInput.sqrMagnitude > 0.01f)
        {
            currentLookDirection =
                camYaw * new Vector3(lookInput.x, 0f, lookInput.y);
        }
    }

    private void UpdateLookFromMouse()
    {
        Ray ray = isoCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            Vector3 toTarget = worldPoint - transform.position;

            if (toTarget.sqrMagnitude > 0.01f)
                currentLookDirection = toTarget.normalized;
        }
    }

    private void DetectActiveDevice()
    {
        InputDevice lastDevice = GetLastUsedDevice();

        if (lastDevice is Gamepad)
        {
            Vector2 move = input.Player.Movement.ReadValue<Vector2>();
            Vector2 look = input.Player.Look.ReadValue<Vector2>();
            if (move.sqrMagnitude > GamepadDeadzone * GamepadDeadzone ||
                look.sqrMagnitude > GamepadDeadzone * GamepadDeadzone)
                usingGamepad = true;
        }
        else if (lastDevice is Mouse)
        {
            usingGamepad = false;
        }
    }

    private InputDevice GetLastUsedDevice()
    {
        InputDevice last = null;
        double lastTime = 0;

        foreach (InputAction action in input.Player.Get())
        {
            InputControl ctrl = action.activeControl;
            if (ctrl == null) continue;

            double t = ctrl.device.lastUpdateTime;
            if (t > lastTime)
            {
                lastTime = t;
                last = ctrl.device;
            }
        }

        return last;
    }

    private void HandleDash()
    {
        Vector3 dashDirection;

        if (moveInput.sqrMagnitude < 0.01f)
        {
            // No input — dash in the direction the player is facing.
            dashDirection = currentLookDirection;
        }
        else
        {
            // Convert the 2D stick input into a camera-relative world direction,
            // matching exactly how HandleMovement works.
            dashDirection = GetCameraYaw() * new Vector3(moveInput.x, 0f, moveInput.y);
            dashDirection = Vector3.ClampMagnitude(dashDirection, 1f);
        }

        // Zero out Y so the dash never has a vertical component.
        dashDirection.y = 0f;

        rb.AddForce(dashDirection.normalized * moveSpeed * 5f, ForceMode.VelocityChange);
    }

    // Returns only the Y-axis (yaw) component of the camera's rotation,
    // flattened onto the XZ plane so pitch doesn't affect movement direction.
    private Quaternion GetCameraYaw()
    {
        if (isoCamera == null)
            return Quaternion.identity;

        Vector3 camForward = isoCamera.transform.forward;
        camForward.y = 0f;

        if (camForward.sqrMagnitude < 0.001f)
            return Quaternion.identity;

        return Quaternion.LookRotation(camForward.normalized);
    }
}