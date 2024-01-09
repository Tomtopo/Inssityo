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
}
