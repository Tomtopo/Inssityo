using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float groundDrag;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _gravity;
    bool _readyToJump = true;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    public LayerMask ground;
    bool _grounded;

    [SerializeField] private Transform orientation;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        _rb.AddForce(Vector3.down * _gravity, ForceMode.Force);
        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, ground);

        SpeedControl();

        if (_grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0f;
    }

    private void MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        if(_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f * _airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    public void OnJumpPressed()
    {
        if (_readyToJump && _grounded)
        {
            _readyToJump = false;

            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), _jumpCooldown);
        }

    }

    private void ResetJump()
    {
        _readyToJump = true;
    }

    public void ReceiveInput(Vector2 movementInput)
    {
        _horizontalInput = movementInput.x;
        _verticalInput = movementInput.y;
    }
}
