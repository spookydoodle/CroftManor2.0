using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 10;
    private Rigidbody rb;

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rb.velocity = this.transform.forward * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        // Destroy the bullet on any collision
        Destroy(this.gameObject);
    }
}
