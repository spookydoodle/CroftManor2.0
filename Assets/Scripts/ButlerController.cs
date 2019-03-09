using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButlerController : MonoBehaviour
{

    public float speed = 1.0f;
    public float rotationSpeed = 75.0f;
    public float jumpForce = 50.0f;
    public float camRayLength = 100f;
    public Animator anim;
    public LayerMask Ground;

    public float maxStepHeight = 0.4f;        // The maximum a player can set upwards in units when they hit a wall that's potentially a step
    public float stepSearchOvershoot = 0.01f; // How much to overshoot into the direction a potential step in units when testing. High values prevent player from walking up tiny steps but may cause problems.

    public Rigidbody _body;
    private bool _isGrounded = true;

    // Use this for initialization
    void Awake()
    {
        Ground = LayerMask.GetMask("Floor");
        Rigidbody _body = gameObject.GetComponent<Rigidbody>();
        Animator anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float f = Input.GetAxisRaw("Fire1");
        float j = Input.GetAxisRaw("Jump");

        Move(v, h, f);
        Jump();
        Animating(v, h);

    }

    void Move(float v, float h, float f)
    {
        if (h != 0f && v < 0f)
        {
            h = -h;
        }

        if (h != 0f && f != 0f)
        {
            MoveSides(h);
        }
        else
        {
            Turn(h);
        }

        MoveForward(v);

    }

    void MoveForward(float v)
    {

        //movement.Set(v, 0f, -h);
        //movement = movement.normalized * speed * Time.deltaTime;
        //butlerRigidbody.MovePosition(transform.position + movement)
        transform.Translate(0, 0, speed * v * Time.deltaTime); ;
                
    }

    void MoveSides(float h)
    {

        //movement.Set(v, 0f, -h);
        //movement = movement.normalized * speed * Time.deltaTime;
        //butlerRigidbody.MovePosition(transform.position + movement)
        transform.Translate(speed * h * Time.deltaTime, 0, 0); ;

    }

    void Turn (float h)
    {
        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit floorHit;

        //if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        //{
        //    Vector3 playerToMouse = floorHit.point - transform.position;
        //    playerToMouse.y = 0f;
        //    Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        //    butlerRigidbody.MoveRotation(newRotation);
        //}

        transform.Rotate(0, rotationSpeed * h * Time.deltaTime, 0);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }


    void Animating (float v, float h)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

}
