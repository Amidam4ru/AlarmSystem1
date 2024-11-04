using UnityEngine;
using UnityEngine.InputSystem;

public class ReadingMotionController : MonoBehaviour
{
    private PlayerController _playerController;

    private Vector2 _lookInput;
    private Vector3 _movementInput;

    public Vector2 LookInput => _lookInput;
    public Vector3 MovementInput => _movementInput;

    private void Awake()
    {
        _playerController = new PlayerController();
    }

    private void OnEnable()
    {
        _playerController.Look.Enable();
        _playerController.Movement.Enable();

        _playerController.Look.Rotation.canceled += OnLook;
        _playerController.Look.Rotation.performed += OnLook;

        _playerController.Movement.Move.performed += OnMove;
        _playerController.Movement.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        _playerController.Look.Rotation.canceled -= OnLook;
        _playerController.Look.Rotation.performed -= OnLook;

        _playerController.Movement.Move.performed -= OnMove;
        _playerController.Movement.Move.canceled -= OnMove;

        _playerController.Look.Disable();
        _playerController.Movement.Disable();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);
    }
}
