using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //[SerializeField] Movement movement;
    //[SerializeField] MouseLook _mouseLook;
    [SerializeField] PlayerCamera _playerCamera;
    [SerializeField] PlayerMovement _movement;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;

    Vector2 movementInput;
    Vector2 mouseDeltaInput;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {    
        OnEnable();
        groundMovement = controls.GroundMovement;

        groundMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();

        groundMovement.Jump.performed += _ => _movement.OnJumpPressed();

        groundMovement.MouseLook.performed += ctx => mouseDeltaInput = ctx.ReadValue<Vector2>();

    }

    private void Update()
    {
        _movement.ReceiveInput(movementInput);
        _playerCamera.ReceiveInput(mouseDeltaInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    //private float _mouseX;
    //private float _mouseY;

    //public void OnRotationX(InputAction.CallbackContext Context)
    //{
    //    _mouseX = Context.ReadValue<float>();
    //}

    //public void OnRotationY(InputAction.CallbackContext Context)
    //{
    //    _mouseY = Context.ReadValue<float>();
    //}


    //public void OnMoveForward(InputAction.CallbackContext Context)
    //{
    //    _forward = Context.ReadValue<float>();
    //    //_moveDir.z = _forward;
    //}
    //public void OnMoveBackward(InputAction.CallbackContext Context)
    //{
    //    _backward = Context.ReadValue<float>();
    //    _moveDir.z = -_backward;
    //}
    //public void OnMoveLeft(InputAction.CallbackContext Context)
    //{
    //    _left = Context.ReadValue<float>();
    //    _moveDir.x = _left;
    //}
    //public void OnMoveRight(InputAction.CallbackContext Context)
    //{
    //    _right = Context.ReadValue<float>();
    //    _moveDir.x = -_right;
    //}
    //public void OnMove(InputAction.CallbackContext Context)
    //{
    //    _moveDir = Context.ReadValue<Vector3>();
    //    Debug.Log(_moveDir);    
    //}

    //public float GetMouseX()
    //{
    //    return _mouseX;
    //}

    //public float GetMouseY()
    //{
    //    return _mouseY;
    //}

    //public float GetForward()
    //{
    //    return _forward;
    //}
    //public float GetBackward()
    //{
    //    return _backward;
    //}
    //public float GetLeft()
    //{
    //    return _left;
    //}
    //public float GetRight()
    //{
    //    return _right;
    //}

    //public Vector3 GetMoveDirection()
    //{
    //    return _moveDir;
    //}

}
