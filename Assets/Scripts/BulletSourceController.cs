using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSourceController : MonoBehaviour
{
    // Available kinds of Bullet objects.
    public List<BaseProjectileController> bullets;
    public GameObject trajectory;
    
    private int currentBulletIndex = 0;
    private float timeBetweenShots = 0.25f;
    private float lastShotTime = 0f;
    private GameObject trajectoryInstance = null;  // FIXME: specific type

    public void Shoot()
    {
        if (this.CanFire())
        {
            Instantiate(this.GetCurrentBullet(), this.transform.position, this.transform.rotation);
            this.lastShotTime = Time.time;
        }
    }

    public void ShowTrajectory()
    {
        if (!this.trajectoryInstance)
        {
            var currentBullet = GetCurrentBullet();
            this.trajectoryInstance = Instantiate(this.trajectory, this.transform);
            TrajectoryController trajectoryController = this.trajectoryInstance.GetComponent<TrajectoryController>();
            trajectoryController.SetInitialSpeed(currentBullet.InitialSpeed());
            trajectoryController.SetWithGravity(currentBullet.UsesGravity());
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

    public void NextBulletKind()
    {
        this.currentBulletIndex = (this.currentBulletIndex + 1) % this.bullets.Count;
    }

    BaseProjectileController GetCurrentBullet()
    {
        return this.bullets[this.currentBulletIndex];
    }
    
    bool CanFire()
    {
        return Time.time - lastShotTime > timeBetweenShots;
    }
}
