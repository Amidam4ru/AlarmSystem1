using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rigidBody;

    private PlayerController _playerController;
    private Vector3 _movementDirection;
    private Vector3 _movementInput;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        _playerController = new PlayerController();
        _playerController.Movement.Move.performed += OnMove;
        _playerController.Movement.Move.canceled += OnMove;
    }

    private void OnEnable()
    {
        _playerController.Movement.Enable();
    }

    private void OnDisable()
    {
        _playerController.Movement.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);
    }

    private void Update()
    {
        Vector3 forward = _cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = _cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        _movementDirection = forward * _movementInput.z + right * _movementInput.x;
    }

    private void FixedUpdate()
    {
        if (_movementDirection.magnitude > 0)
        {
            _rigidBody.MovePosition(_rigidBody.position + _movementDirection * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
