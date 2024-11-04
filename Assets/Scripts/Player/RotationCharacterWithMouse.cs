using UnityEngine;

[RequireComponent(typeof(ReadingMotionController))]
public class RotationCharacterWithMouse : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _playerTransform;

    private float _xRotation = 0f;
    private ReadingMotionController _controller;
    private Vector2 _lookInput;

    private void Awake()
    {
        _controller = GetComponent<ReadingMotionController>();
    }

    private void Update()
    {
        _lookInput = _controller.LookInput;
        float mouseX = _lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * _mouseSensitivity * Time.deltaTime;

        _playerTransform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}