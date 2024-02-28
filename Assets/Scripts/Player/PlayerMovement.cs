using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject _playerObject;

    [Header("Movement")]
    private float _moveSpeed;
    [SerializeField] private float _crouchMoveSpeed = 5f;
    [SerializeField] private float _walkMoveSpeed = 10f;

    [SerializeField] private float groundDrag;
    [SerializeField] private float wallDrag;
    [SerializeField] private float wallCheckRadius;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _gravity;
    bool _readyToJump = true;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    public LayerMask ground;
    public LayerMask wall;
    bool _grounded;
    bool _walled;

    public bool isCrouching = false;

    [SerializeField] private Transform orientation;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _playerObject = transform.GetChild(0).gameObject;
        _moveSpeed = _walkMoveSpeed;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, wallCheckRadius);
    //}

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        _rb.AddForce(Vector3.down * _gravity, ForceMode.Force);
        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, ground);
        //RaycastHit hit;
        //_walled = Physics.SphereCast(transform.position, wallCheckRadius, Vector3.up, out hit, wallCheckRadius, wall);
        //Debug.Log(hit.collider);

        SpeedControl();

        if (_grounded)
            _rb.drag = groundDrag;
        else if (_walled)
            _rb.drag = wallDrag;
        else
            _rb.drag = 0f;
    }

    private void MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        if(_grounded)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
        else if(!_grounded)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if(flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
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

    public void OnCrouchPressed()
    {
        isCrouching = true;
        _moveSpeed = _crouchMoveSpeed;
        _playerObject.GetComponent<CapsuleCollider>().height = 1f;
    }

    public void OnCrouchReleased()
    {
        isCrouching = false;
        _moveSpeed = _walkMoveSpeed;
        _playerObject.GetComponent<CapsuleCollider>().height = 2f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            _walled = true;
            _rb.drag = wallDrag;
        }
        else
            _walled = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            _walled = true;
            _rb.drag = wallDrag;
        }
        else
            _walled = false;
    }
}
