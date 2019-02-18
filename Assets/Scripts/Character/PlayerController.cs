using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    public float speed = 0f;
    public float acceleration = 0.5f;
    public float maxSpeed = 5f;
    public float deceleration = 1f;
    public float mouseSensitivity = 5f;
    public float jumpForce = 500f;
    public float heigth;
    public float xDirection, zDirection; // Vectores de direccion
    private float yRotation, xRotation; // vectores de movimiento de camara
    public bool jump, _isGrounded, sprint;
    LayerMask Ground;
    Vector3 velocity, lastVelocity, rotation, jumpVector, camRotation, 
            origin, moveSideways, moveForward;
    public PlayerMovement motor;

    public void Start()
    {
        motor.GetComponent<PlayerMovement>();
        Ground = (1 << LayerMask.NameToLayer("Ground"));
    }

    public void Update()
    {
        //Sets all of the numbers needed.
        InputSetter();
        Movimiento();
        JumpVectorSetter(jump);
        IsGrounded();
        
        // Calls for the methods on PlayreMovement
        motor.Move(velocity);
        motor.Rotate(rotation);
        motor.RotateCamera(camRotation);
        motor.Jump(jumpVector, _isGrounded);

    }
    private void IsGrounded()
    {
         _isGrounded = Physics.Raycast(transform.position + new Vector3(0, -heigth / 2, 0), Vector3.down, 1f, Ground);
        
    }

    private void InputSetter()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        zDirection = Input.GetAxisRaw("Vertical");
        yRotation = Input.GetAxisRaw("Mouse X");
        xRotation = Input.GetAxisRaw("Mouse Y");
        jump = Input.GetButtonDown("Jump");
        sprint = Input.GetButton("Sprint");
    }

    private void JumpVectorSetter(bool jump)
    {
        if (jump)
        {
            jumpVector = Vector3.up * jumpForce;
        }
        else { jumpVector = Vector3.zero; }
    }

    private void Movimiento()
    {
        moveSideways = transform.right * xDirection;
        moveForward = transform.forward * zDirection; // Transform.forward va a dar el vector de transformacion del objeto usando los angulos locales

        if (zDirection != 0 || xDirection != 0)
        {
            if (sprint)
            {
                maxSpeed = 20f;
                acceleration = 1f;
                if (speed <= maxSpeed)
                {
                    speed += acceleration;
                }
                velocity = (moveSideways + moveForward).normalized * speed;
                deceleration = 1f;
            }
            else
            {
                maxSpeed = 5f;
                if (speed <= maxSpeed)
                {
                    speed += acceleration;
                }
                velocity = (moveSideways + moveForward).normalized * speed;
                deceleration = 2f;
            }
            lastVelocity = velocity / speed;
        }
        else
        {
            if (speed > 0)
            {
                speed -= deceleration;
            }
            else { speed = 0; }

            velocity = lastVelocity * speed;
        }

        rotation = new Vector3(0, yRotation, 0) * mouseSensitivity;
        camRotation = new Vector3(xRotation, 0, 0) * mouseSensitivity;
    }
}