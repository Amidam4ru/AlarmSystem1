using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ReadingMotionController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rigidBody;
    private ReadingMotionController _controller;
    private Vector3 _movementDirection;
    private Vector3 _movementInput;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _controller = GetComponent<ReadingMotionController>();
    }

    private void Update()
    {
        _movementInput = _controller.MovementInput;

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