using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController controller;
    private PlayerInputActions input;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = new PlayerInputActions();
    }
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDsiable()
    {
        input.Disable();
    }

    private void Update()
    {
        Vector2 moveInput = input.Player.Movement.ReadValue<Vector2>();

        Vector3 moveDirection =
            new Vector3(moveInput.x, 0f, moveInput.y);

        controller.Move(
            moveDirection * moveSpeed * Time.deltaTime
        );
    }
}