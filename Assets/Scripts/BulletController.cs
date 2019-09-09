using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private static float speed = 10;
    private Rigidbody rb;

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rb.velocity = this.transform.forward * speed;
        this.rb.useGravity = this.UsesGravity();
    }

    public Vector3 InitialSpeed()
    {
        return new Vector3(0, 0, speed);
    }

    public bool UsesGravity()
    {
        return false;
    }

    void OnCollisionEnter(Collision other)
    {
        // Destroy the bullet on any collision
        Destroy(this.gameObject);
    }
}
