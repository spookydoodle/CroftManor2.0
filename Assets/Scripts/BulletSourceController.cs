using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSourceController : MonoBehaviour
{
    public GameObject bullet;
    
    private float timeBetweenShots = 0.25f;
    private float lastShotTime = 0f;

    public void Shoot()
    {
        if (this.CanFire())
        {
            Instantiate(this.bullet, this.transform.position, this.transform.rotation);
            this.lastShotTime = Time.time;
        }
    }
    
    bool CanFire()
    {
        return Time.time - lastShotTime > timeBetweenShots;
    }
}
