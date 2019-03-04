using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButlerController : MonoBehaviour
{

    public Animator anim;
    public float speed = 1.0f;
    public float rotationSpeed = 75.0f;

    // Use this for initialization
    void Start()
    {

        Animator anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) 
            || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsWalking", true);
            transform.Translate(speed * Input.GetAxis("Vertical") * Time.deltaTime, 0, 0);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
        }
    }
}
