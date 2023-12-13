using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //[SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;
    Rigidbody _rb;

    [SerializeField] float jumpHeight = 3.5f;
    [SerializeField] float gravityForce = 20;
    [SerializeField]bool jump;
    //[SerializeField] float gravity = -9f;
    //Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    [SerializeField]bool isGrounded;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z), 0.05f);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z), 0.05f, groundMask);
        //if(isGrounded)
        //{
        //    verticalVelocity.y = 0f;
        //}

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        //transform.Translate(horizontalVelocity * Time.deltaTime);
        _rb.AddForce(horizontalVelocity/* * Time.deltaTime*/);

        if(jump)
        {
            if(isGrounded)
            {
                //verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                //verticalVelocity.y += gravity * Time.deltaTime;
                _rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            }
            jump = false;
        }

        //verticalVelocity.y += gravity * Time.deltaTime;
        //transform.Translate(verticalVelocity * Time.deltaTime);

        _rb.AddForce(Vector3.down * gravityForce);
    }

    public void ReceiveInput(Vector2 _horizontalinput)
    {
        horizontalInput = _horizontalinput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
