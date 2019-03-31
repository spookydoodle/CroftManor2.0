using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    // Constants
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 75.0f;
    public float jumpSpeed = 25.0f;
    public float gravity = 5.0f;
    public float buoyancy = 4.0f;

    // State
    private bool isSwimming = false;

    // MovementState
    Vector3 speed = Vector3.zero;

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
        float f = Input.GetAxisRaw("Fire1");
        bool j = Input.GetButtonDown("Jump");

        if (j)
        {
            Jump();
        }

        Fall();
        
        MoveForward(v);

        // Reverse rotation when moving backwards
        if (h != 0f && v < 0f)
        {
            h = -h;
        }

        // Combination ctrl and Right/Left (D, A) arrows - moves sideways, otherwise rotate
        if (h != 0f && f != 0f)
        {
            MoveSideways(h);
        }
        else
        {
            Rotate(h);
        }
        
        Move();

        // Set animations for movement
        Animating(v, h);
    }

    void MoveForward(float v)
    {
        speed.z = moveSpeed * v;
    }

    void MoveSideways(float h)
    {
        speed.x = h;
    }

    void Rotate(float h)
    {
        transform.Rotate(0.0f, rotationSpeed * h * Time.deltaTime, 0.0f);
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
        _controller.Move(transform.TransformDirection((speed * Time.deltaTime)));
    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating(float v, float h)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
