using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private InputSystem_Actions inputActions;
    private Vector2 movementInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    private void Update() {
        movementInput = inputActions.Player.Move.ReadValue<Vector2>();

        if (movementInput.sqrMagnitude > 1f) {
            movementInput.Normalize();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }
}