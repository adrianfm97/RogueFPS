using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    /********Variable Declarations BEGIN ********/

    public PlayerMovement motor;

    [Header("Movement")]
    public float speed = 0f;
    public float acceleration = 0.5f;
    public float maxSpeed = 5f;
    public float deceleration;
    private float xDirection,
                  zDirection;
    private bool sprint;
    Vector3 velocity,
            lastVelocity,
            moveSideways, 
            moveForward;

    [Header("CameraMovement")]
    public float mouseSensitivity = 5f;
    private float yRotation,
                  xRotation;
    Vector3 rotation,
            camRotation;

    [Header("Jump")]
    public float jumpForce = 500f;
    public float Rayheigth = -0.99f;
    private bool jump;
    public bool _isGrounded;
    private float rayCastLength = 0.1f;
    private Vector3 rayCastOrigin;
    LayerMask Ground;
    Vector3 jumpVector;

    [Header("Headbobb")]
    public float bobbingSpeed;
    public float bobbingAmount;
    private float timerY;
    private float xBobbing, yBobbing;
    private float midpoint;
    private float totalBobbing;
    private float bobbingMovementY = 0.0f;
    private float bobbingMovementX = 0.0f;
    private float wavesliceY,
                 wavesliceX;
    

    /********Variable Declarations END ********/


    public void Start()
    {
        motor.GetComponent<PlayerMovement>();
        Ground = (1 << LayerMask.NameToLayer("Ground"));
    }

    public void Update()
    {
        InputSetter();
        JumpVectorSetter(jump);
        IsGrounded();
        headBobb();
    }

    public void FixedUpdate()
    {
        //Sets all of the numbers needed.
        Movimiento();
        
        // Calls for the methods on PlayerMovement
        motor.Move(velocity);
        motor.Rotate(rotation);
        motor.RotateCamera(camRotation);
        motor.Jump(jumpVector, _isGrounded);
        motor.headBobb(bobbingMovementX, bobbingMovementY);
    }
    private void IsGrounded()
    {
        rayCastOrigin = transform.position + new Vector3(0, Rayheigth, 0);
        _isGrounded = Physics.Raycast(rayCastOrigin, Vector3.down, rayCastLength, Ground);
        Debug.DrawLine(rayCastOrigin , rayCastOrigin + Vector3.down * rayCastLength, Color.red);

    }

    private void InputSetter()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        zDirection = Input.GetAxisRaw("Vertical");
        yRotation = Input.GetAxisRaw("Mouse X");
        xRotation = Input.GetAxisRaw("Mouse Y");
        jump = Input.GetButtonDown("Jump");
        sprint = Input.GetButton("Sprint");
        xBobbing = Input.GetAxis("Horizontal");
        yBobbing = Input.GetAxis("Vertical");
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
            }
            else
            {
                maxSpeed = 5f;
                if (speed <= maxSpeed)
                {
                    speed += acceleration;
                }
                velocity = (moveSideways + moveForward).normalized * speed;
            }
            lastVelocity = velocity / speed;
        }
        else
        {
            if (speed > 0)
            {
                if (_isGrounded)
                {
                    speed -= deceleration;
                }
            }
            else { speed = 0; }

            velocity = lastVelocity * speed;
        }

        rotation = new Vector3(0, yRotation, 0) * mouseSensitivity;
        camRotation = new Vector3(xRotation, 0, 0) * mouseSensitivity;
    }
    
    private void headBobb()
    {
        wavesliceY = 0.0f;
        wavesliceX = 0.0f;

        if (sprint)
        {
            bobbingSpeed = 7f;
            bobbingAmount = 0.28f;
        }
        else
        {
            bobbingSpeed = 5f;
            bobbingAmount = 0.2f;
        }

        if (Mathf.Abs(xBobbing) == 0 && Mathf.Abs(yBobbing) == 0)
        {
            timerY = Mathf.PI / 2;
            bobbingMovementY = midpoint;
        }
        else
        {
            wavesliceY= Mathf.Abs(Mathf.Sin(timerY));
            wavesliceX = Mathf.Cos(timerY);
            timerY += bobbingSpeed * Time.deltaTime;
        }

        if (wavesliceY != 0)
        {
            totalBobbing = Mathf.Abs(xBobbing) + Mathf.Abs(yBobbing);
            totalBobbing = Mathf.Clamp(totalBobbing, 0.0f, 1.0f);
            bobbingMovementY = midpoint + (totalBobbing * wavesliceY * bobbingAmount);
            if (sprint)
            {
                bobbingMovementX = midpoint + (totalBobbing * wavesliceX * bobbingAmount);
            }
        }
        else
        {
            bobbingMovementY = midpoint;
            bobbingMovementX = 0.0f;
        }
        

    }
}