using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSourceController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject trajectory;
    
    private float timeBetweenShots = 0.25f;
    private float lastShotTime = 0f;
    private GameObject trajectoryInstance = null;

    public void Shoot()
    {
        if (this.CanFire())
        {
            Instantiate(this.bullet, this.transform.position, this.transform.rotation);
            this.lastShotTime = Time.time;
        }
    }

    public void ShowTrajectory()
    {
        if (!this.trajectoryInstance)
        {
            this.trajectoryInstance = Instantiate(this.trajectory, this.transform);
        }
    }

    public void HideTrajectory()
    {
        if (this.trajectoryInstance)
        {
            Destroy(this.trajectoryInstance.gameObject);
            this.trajectoryInstance = null;
        }
    }
    
    bool CanFire()
    {
        return Time.time - lastShotTime > timeBetweenShots;
    }
}
