using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButlerController : MonoBehaviour
{

    public float speed = 1.0f;
    public float rotationSpeed = 75.0f;
    public float camRayLength = 100f;
    public Animator anim;
    public Rigidbody butlerRigidbody;

    Vector3 movement;
    int floorMask;



    // Use this for initialization
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        Rigidbody butlerRigidbody = gameObject.GetComponent<Rigidbody>();
        Animator anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(v, h);
        Animating(v, h);

    }

    void Move(float v, float h)
    {
        if(h != 0f && v != 0f)
        {
            MoveForward(v, h);
            Turn(h);
        }
        else if (h == 0f && v != 0f)
        {
            MoveForward(v, h);
        }

        else if(h != 0f && v == 0f)
        {
            MoveSides(v, h);
        }
    }

    void MoveForward(float v, float h)
    {

        //movement.Set(v, 0f, -h);
        //movement = movement.normalized * speed * Time.deltaTime;
        //butlerRigidbody.MovePosition(transform.position + movement)
        transform.Translate(speed * v * Time.deltaTime, 0, 0); ;
                
    }

    void MoveSides(float v, float h)
    {

        //movement.Set(v, 0f, -h);
        //movement = movement.normalized * speed * Time.deltaTime;
        //butlerRigidbody.MovePosition(transform.position + movement)
        transform.Translate(0, 0, -speed * h * Time.deltaTime); ;

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

    void Animating (float v, float h)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

}
