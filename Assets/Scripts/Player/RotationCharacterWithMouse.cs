using UnityEngine;
using UnityEngine.InputSystem;

public class RotationCharacterWithMouse : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _playerTransform;

    private float _xRotation = 0f;

    private PlayerController _playerController;
    private InputAction _lookAction;
    private Vector2 _lookInput;

    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.Look.Rotation.canceled += OnLook;
        _playerController.Look.Rotation.performed += OnLook;
    }

    private void OnEnable()
    {
        _playerController.Look.Enable();
    }

    private void OnDisable()
    {
        _playerController.Look.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        float mouseX = _lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * _mouseSensitivity * Time.deltaTime;

        _playerTransform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}