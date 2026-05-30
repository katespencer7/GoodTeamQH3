using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Camera")]
    [SerializeField] private Camera isoCamera;

    private CharacterController controller;
    private PlayerInputActions input;

    private Vector3 currentLookDirection = Vector3.forward;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = new PlayerInputActions();

        if (isoCamera == null)
            isoCamera = Camera.main;
    }

    private void OnEnable()  => input.Enable();
    private void OnDisable() => input.Disable();

    [SerializeField] private bool usingGamepad = false;

    private void Update()
    {
        DetectActiveDevice();

        Vector2 moveInput = input.Player.Movement.ReadValue<Vector2>();

        // Build a flat rotation that matches the camera's horizontal yaw.
        // This maps stick axes into isometric world space correctly.
        Quaternion camYaw = GetCameraYaw();

        //
        // Movement
        //
        Vector3 moveDirection = camYaw * new Vector3(moveInput.x, 0f, moveInput.y);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        //
        // Aiming / Facing
        //
        if (usingGamepad)
            UpdateLookFromStick(camYaw);
        else
            UpdateLookFromMouse();

        Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void DetectActiveDevice()
    {
        // Switch to gamepad if any gamepad input is detected.
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            usingGamepad = true;
        }
        // Switch back to mouse if the mouse moves or clicks.
        else if (Mouse.current != null &&
                 (Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f ||
                  Mouse.current.leftButton.wasPressedThisFrame))
        {
            usingGamepad = false;
        }
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
        // Cast a ray from the mouse position onto the XZ ground plane.
        Ray ray = isoCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            Vector3 toTarget = worldPoint - transform.position;

            // Only update if the cursor isn't sitting directly on the player.
            if (toTarget.sqrMagnitude > 0.01f)
                currentLookDirection = toTarget.normalized;
        }
    }

    // Returns only the Y-axis (yaw) component of the camera's rotation,
    // flattened onto the XZ plane so pitch doesn't affect movement direction.
    private Quaternion GetCameraYaw()
    {
        if (isoCamera == null)
            return Quaternion.identity;

        Vector3 camForward = isoCamera.transform.forward;

        // Flatten: zero out the Y component and renormalize.
        camForward.y = 0f;

        // Guard against a perfectly vertical camera (directly overhead).
        if (camForward.sqrMagnitude < 0.001f)
            return Quaternion.identity;

        return Quaternion.LookRotation(camForward.normalized);
    }
}