using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    // Constants
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 125.0f;
    public float jumpSpeed = 25.0f;
    public float gravity = 5.0f;
    public float buoyancy = 4.0f;
    public float mouseSensitivity = 5f;

    // State
    private bool isSwimming = false;

    // MovementState
    Vector3 speed = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    // Unity
    CharacterController _controller;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Water")
       {
           isSwimming = true;
       }
    }

    void OnTriggerExit(Collider other)
    {
       if (other.gameObject.tag == "Water")
       {
           isSwimming = false;
       }
    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool j = Input.GetButtonDown("Jump");
        float x = Input.GetAxis("Mouse X");
        
        if (j)
        {
            Jump();
        }

        Fall();
        
        HandleRotation(x * Settings.mouseSensitivity());
        
        HandleSideMovement(h);
        
        MoveForward(v);

        Move();

        // Set animations for movement
        Animating(v, h);
    }

    void MoveForward(float v)
    {
        speed.z = moveSpeed * v;
    }
    void HandleRotation(float rotationValue)
    {
        rotation.y = rotationValue * rotationSpeed;
    }

    void HandleSideMovement(float inputValue)
    {
        speed.x = moveSpeed * inputValue;
    }

    void Jump()
    {
        if (isSwimming) {
            speed.y += jumpSpeed;
        } else if (_controller.isGrounded) {
            speed.y = jumpSpeed;
        }
    }

    void Fall()
    {
        if (!_controller.isGrounded) {
          speed.y -= gravity;
            if (isSwimming) {
                speed.y += buoyancy;
            }
        }
    }

    void Move()
    {
        // Apply movement vectors
        _controller.Move(transform.TransformDirection(speed * Time.deltaTime));
        transform.Rotate(rotation * Time.deltaTime);
    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating(float v, float h)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
