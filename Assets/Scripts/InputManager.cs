using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerCamera _playerCamera;
    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerInteraction _interaction;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.InteractionActions interactionActions;

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
        interactionActions = controls.Interaction;

        groundMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();

        groundMovement.Jump.performed += _ => _movement.OnJumpPressed();

        groundMovement.Crouch.performed += _ => _movement.OnCrouchPressed();
        groundMovement.Crouch.canceled += _ => _movement.OnCrouchReleased();

        groundMovement.MouseLook.performed += ctx => mouseDeltaInput = ctx.ReadValue<Vector2>();

        interactionActions.Interaction.performed += _ => _interaction.OnInteraction();
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
