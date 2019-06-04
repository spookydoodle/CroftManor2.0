﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    // Constants
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float jumpSpeed = 25.0f;
    public float gravity = 5.0f;
    public float buoyancy = 4.0f;

    // State
    private bool isSwimming = false;

    // MovementState
    Vector3 speed = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    // Unity
    CharacterController _controller;
    public Animator anim;
    private Camera cameraObject;
    public BulletSourceController bulletSourceController;

    // Use this for initialization
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        cameraObject = Camera.main;
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
        bool f = Input.GetButton("Fire1");
        bool f3WentUp = Input.GetButtonUp("Fire3");
        bool f3WentDown = Input.GetButtonDown("Fire3");

        // Up-Down movement
        if (j)
        {
            Jump();
        }

        Fall();
        
        // Left-Right movement
        HandleRotation(v, h, cameraObject, _controller);

        // Front movement
        MoveForward(v, h);

        // Apply computed movement
        Move();

        // Shoot
        if (f) {
            this.bulletSourceController.Shoot();
        }

        // Show trajectory
        if (f3WentDown)
        {
            this.bulletSourceController.ShowTrajectory();
        }
        else if (f3WentUp)
        {
            this.bulletSourceController.HideTrajectory();
        }

        // Set animations for movement
        Animating(v, h);
    }

    void MoveForward(float frontBack, float leftRight)
    {
        // Speed depends on the input with the largest amplitude, i.e.
        // Trigger pushed all the way up or all the way left will result in the same speed
        //float magnitude = Mathf.Max(Mathf.Abs(frontBack), Mathf.Abs(leftRight));
        float magnitude = Mathf.Max(Mathf.Abs(frontBack), Mathf.Abs(leftRight));
        speed.z = moveSpeed * magnitude;
    }

    void HandleRotation(float frontBack, float leftRight, Camera camera, CharacterController butler)
    {
        // Rotation is based on the ratio of frontBack and leftRight inputs
        // If frontBack is 1 and leftRight is 1, the controller will rotate by 45 degrees to the left.
        float angle = Mathf.Atan2(leftRight, frontBack) * Mathf.Rad2Deg;
        bool movingBackwards = frontBack < 0;
        rotation.y = angle;
        //if (!movingBackwards)
        //{
        //    rotation.y = angle;
        //}
        //else
        //{
        //    rotation.y = -angle;
        //}
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
        // Only take camera's Y rotation into account
        Vector3 cameraRotationOffset = new Vector3(0, cameraObject.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(rotation + cameraRotationOffset);

        // Apply movement vectors
        _controller.Move(transform.TransformDirection(speed * Time.deltaTime));
    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating(float v, float h)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    public bool IsMoving()
    {
        return this.speed.z != 0f;
    }
}
