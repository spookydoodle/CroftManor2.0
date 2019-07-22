using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraOperatorController : MonoBehaviour {
    public bool invertYaxis = false;
    public ButlerPovController target;

    private float rotationSpeed = 2.5f;
    private int setInvYax;
    private ArmController arm;
    private Follow follow;
    private Camera camera;

    void Start()
    {
        arm = gameObject.GetComponentInChildren<ArmController>();
        camera = gameObject.GetComponentInChildren<Camera>();
        follow = gameObject.GetComponent<Follow>();
        handleSettings(invertYaxis);
    }

    void Update()
    {
        float y = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Mouse X");

        HandleRotation(setInvYax * y * Settings.MouseSensitivity(), x * Settings.MouseSensitivity());

        if (ShouldAutoAlign())
        {
            follow.SetFollowRotation(true);
            Align();
        }
        else
        {
            follow.SetFollowRotation(false);
        }

        // FIXME: this script should have full control over movement -> use Follow in manual mode
        float obstacleDistance = ObstacleDistance();
        if (obstacleDistance != -1f)
        {
            arm.SetRadius(obstacleDistance - 0.1f);  // Set the camera radius a little bit in front of the obstacle
        }
    }

    void HandleRotation(float rotationX, float rotationY)
    {
        arm.Rotate(rotationX * rotationSpeed, rotationY * rotationSpeed);
    }

    public void Align()
    {
        arm.SmoothlyResetRotation();
    }

    private bool ShouldAutoAlign()
    {
        if (!target.isMoving) {
            return false;
        }

        // The camera should not follow if the character is facing the camera
        Vector3 diff = target.transform.rotation.eulerAngles - camera.transform.rotation.eulerAngles;
        float angle = Mathf.Abs(diff.y);

        float range = 30f;
        float backwardsAngle = 180f;  // 180 degrees -> character is looking at the camera
        bool isLookingAtCamera = angle > (backwardsAngle - range) && angle < (backwardsAngle + range);

        return !isLookingAtCamera;
    }

    void handleSettings(bool invertYaxis)
    {
        if (invertYaxis)
            setInvYax = -1;
        else
            setInvYax = 1;
    }

    // Detects objects between target and the camera
    RaycastHit[] DetectCollisions()
    {
        Vector3 targetPosition = follow.target.transform.position;
        Vector3 cameraPosition = camera.transform.position;
        return Physics.RaycastAll(targetPosition, cameraPosition - targetPosition, arm.optimalRadius);
    }

    // Returns distance to the nearest object considered an obstacle
    // Returns -1 if no obstacle found
    float ObstacleDistance()
    {
        var collisions = DetectCollisions();
        float[] obstacles = (
          from collision in collisions
          where IsObstacle(collision.collider.gameObject)
          select collision.distance
        ).ToArray();

        return obstacles.Any() ? obstacles.Min() : -1f;
    }

    bool IsObstacle(GameObject gameObject)
    {
        return gameObject.tag != "Player";
    }
}
