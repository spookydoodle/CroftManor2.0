using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    public float speed = 1.5f;
    public float rotationSpeed = 75.0f;
    public float jumpSpeed = 50.0f;
    public float gravity = 20.0f;
    float playerGravity;
    private bool isGrounded = false;

    CharacterController _controller;
    public Animator anim;
    Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        Animator anim = gameObject.GetComponent<Animator>();
        playerGravity = -gravity;

    }


    // Detect collision with floor
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Floor")
            isGrounded = true;
    }
    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.transform.tag == "Floor")
    //        isGrounded = false;
    //}


    // Update is called once per frame
    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float f = Input.GetAxisRaw("Fire1");
        bool j = Input.GetButtonDown("Jump");


        Debug.Log(_controller.isGrounded);

        // Fall downwards
        Fall();

        // Move forwards/backwards
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

        // Jump if key is pressed
        if (j)
        {
            Jump(j, jumpSpeed);
        }

        // Check if player is grounded AFTER movement
        setGravity();
        

        // Set animations for movement
        Animating(v, h);

    }


    void setGravity()
    {
        if (_controller.isGrounded)
            playerGravity = 0;
        else
            playerGravity = -gravity;
    }


    void MoveForward(float v)
    {
        //if (_controller.isGrounded)
        //{
            moveDirection = new Vector3(0, 0, v);
            moveDirection = speed * transform.TransformDirection(moveDirection);
            transform.Translate(0, 0, speed * v * Time.deltaTime);
        //}

        _controller.Move(moveDirection * Time.deltaTime);
    }


    void MoveSideways(float h)
    {
        //if (_controller.isGrounded)
        //{
            moveDirection = new Vector3(h, 0, 0);
            moveDirection = speed * transform.TransformDirection(moveDirection);
            transform.Translate(speed * h * Time.deltaTime, 0, 0);
        //}

        _controller.Move(moveDirection * Time.deltaTime);

    }


    void Rotate(float h)
    {
        transform.Rotate(0.0f, rotationSpeed * h * Time.deltaTime, 0.0f);
    }


    void Jump(bool j, float jumpSpeed)
    {
        moveDirection = new Vector3(0.0f, jumpSpeed, 0.0f);
        moveDirection = transform.TransformDirection(moveDirection);

        _controller.Move(moveDirection * Time.deltaTime);
    }


    void Fall()
    {
        moveDirection = new Vector3(0, playerGravity, 0);
        _controller.Move(moveDirection * Time.deltaTime);
    }


    // Set parameters used in conditions of transitions in Animator component
    void Animating(float v, float h)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

}
