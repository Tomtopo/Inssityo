using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField]Vector2 _mouseDelta;
    [SerializeField]float _mouseSensitivity = 20f;
    [SerializeField]GameObject _playerCamera;
    Quaternion _originalRotation;

    private void Start()
    {
        _playerCamera.transform.rotation = Quaternion.identity;
        _originalRotation = transform.rotation;
    }

    void Update()
    {
        Debug.Log(_playerCamera.transform.rotation);

        _mouseDelta *= _mouseSensitivity * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(_playerCamera.transform.localRotation.x, _originalRotation.y, _originalRotation.z);
        Quaternion verticalRot = Quaternion.Euler(-_mouseDelta.y, _originalRotation.y, _originalRotation.z);
        Quaternion horizontalRot = Quaternion.Euler(0f, _mouseDelta.x, 0f);
        rotation *= verticalRot;
        _playerCamera.transform.localRotation *= verticalRot;
        transform.rotation *= horizontalRot;
    }

    public void ReceiveInput(Vector2 mouseDeltaInput)
    {
        _mouseDelta = mouseDeltaInput;
    }
}
