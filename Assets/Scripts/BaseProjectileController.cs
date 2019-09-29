using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for Projectiles.
// Implements the common logic, leaving the specifics to the derived class.
// Note: this class is abstract. It can't be used directly. To use its functionality,
// one needs to write a child class inheriting from this one.
public abstract class BaseProjectileController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rb.useGravity = this.UsesGravity();
        
        var speed = InitialSpeed();
        this.rb.velocity = this.transform.forward * speed.z + this.transform.up * speed.y + this.transform.right * speed.x;
    }

    // These methods dictate how the projectile behaves.
    // They should be implemented in derived classes.
    public abstract Vector3 InitialSpeed();
    public abstract bool UsesGravity();
}
