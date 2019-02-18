using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    // Use this for initialization
    private Rigidbody rb;
    public Camera cam;
    Vector3 xRotation, yRotation;
    Vector3 velocity = Vector3.zero;
    Vector3 jumpVector = Vector3.zero;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        performMovement();
        performRotation();
	}

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _xRotation)
    {
        xRotation = _xRotation;
    }

    public void RotateCamera(Vector3 _yRotation)
    {
        yRotation = _yRotation;
    }

    public void Jump(Vector3 _jumpVector, bool _isGrounded)
    {
        if (_isGrounded)
        {
            jumpVector = _jumpVector;
        }
        else { jumpVector = Vector3.zero; }
    }

    public void performMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (jumpVector != Vector3.zero)
        { 
            rb.AddForce(jumpVector * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    public void performRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(xRotation));
        cam.transform.Rotate(- yRotation);
    }

}
