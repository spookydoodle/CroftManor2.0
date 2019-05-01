using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public GameObject target;  // The position that that camera will be following.
    public bool invertYaxis = false;

    private PlayerCharacterController playerController;
    private float smoothing = 2f;
    private float rotationSpeed = 2.5f;
    private Vector3 basePositionOffset;  // The base distance from the target.
    private Vector3 baseRotationOffset;  // The base rotation relative to the target.
    private Vector3 extraRotation = Vector3.zero;  // Extra rotation (e.g. caused by user input)
    private int setInvYax;

    void Start()
    {
        playerController = target.gameObject.GetComponent<PlayerCharacterController>();
        basePositionOffset = target.transform.position - transform.position;
        baseRotationOffset = target.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
        handleSettings(invertYaxis);
    }

    void Update()
    {
        float y = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Mouse X");
        HandleRotation(setInvYax * y * Settings.MouseSensitivity(), x * Settings.MouseSensitivity());

        if (this.shouldFollow())
        {
            Follow();
        }

        Move();
    }

    void HandleRotation(float rotationX, float rotationY)
    {
        extraRotation.x += rotationX * rotationSpeed;
        extraRotation.y += rotationY * rotationSpeed;
        extraRotation.x = Mathf.Clamp(extraRotation.x, -15f, 50f);
    }

    bool shouldFollow()
    {
        // The camera should not follow if the character is facing the camera
        Vector3 diff = this.target.transform.rotation.eulerAngles - this.transform.rotation.eulerAngles;
        float angle = Mathf.Abs(diff.y);
        
        float range = 30f;
        float backwardsAngle = 180f;  // 180 degrees -> character is looking at the camera
        bool isLookingAtCamera = angle > (backwardsAngle - range) && angle < (backwardsAngle + range);

        return this.playerController.IsMoving() && !isLookingAtCamera;
    }

    void Follow()
    {
        float lerp = Time.deltaTime * smoothing;
        float yAngle = Mathf.LerpAngle(extraRotation.y, target.transform.rotation.eulerAngles.y, lerp);
        extraRotation.y = yAngle;
    }

    void Move()
    {
        Quaternion newRotation = Quaternion.Euler(baseRotationOffset + extraRotation);
        this.transform.SetPositionAndRotation(target.transform.position, newRotation);
        
        // Translate by the offset *after* setting the rotation -> translation takes rotation into account
        this.transform.Translate(-basePositionOffset);
    }

    void handleSettings(bool invertYaxis)
    {
        if (invertYaxis)
            setInvYax = -1;
        else
            setInvYax = 1;
    }
}
