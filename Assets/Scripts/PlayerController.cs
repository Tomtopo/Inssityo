using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerInput _playerInput;
    [SerializeField]private InputManager _inputManager;
    [SerializeField]private Camera _camera;

    //private float _mouseX;
    //private float _mouseY;

    [SerializeField] private float _horizontalSensitivity = 30f;
    [SerializeField]private float _verticalSensitivity = 30f;
    [SerializeField] private float _moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //_playerInput = GetComponent<PlayerInput>();
        _inputManager = GetComponent<InputManager>();
        Vector3 defaultCamPos = new Vector3(0, 0, 0);
        _camera.transform.rotation = Quaternion.Euler(defaultCamPos);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    float RotationX = _horizontalSensitivity * _inputManager.GetMouseX() * Time.deltaTime;
    //    float RotationY = _verticalSensitivity * _inputManager.GetMouseY() * Time.deltaTime;

    //    Vector3 CameraRotation = _camera.transform.rotation.eulerAngles;
    //    Vector3 PlayerRotation = transform.rotation.eulerAngles;

    //    CameraRotation.x -= RotationY;
    //    CameraRotation.y += RotationX;

    //    PlayerRotation.x -= RotationY;
    //    PlayerRotation.y += RotationX;

    //    _camera.transform.rotation = Quaternion.Euler(CameraRotation);
    //    transform.rotation = Quaternion.Euler(new Vector3(0, PlayerRotation.y, 0));

    //    transform.Translate(_inputManager.GetMoveDirection() * _moveSpeed * Time.deltaTime);
    //}

}
