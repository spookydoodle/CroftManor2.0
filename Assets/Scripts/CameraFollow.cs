using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public GameObject target;  // The position that that camera will be following.
    
    private float rotationSpeed = 1f;
    private Vector3 offset;  // The initial offset from the target.
    private Vector3 rotation = Vector3.zero;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void Update()
    {
        float y = Input.GetAxis("Mouse Y");
        HandleRotation(y * Settings.MouseSensitivity());
        SetCameraTransform();
    }

    void HandleRotation(float rotationValue)
    {
        rotation.x += rotationValue * rotationSpeed;

        // Limit the rotation to <-45: Down, 90: Up>
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
    }

    void SetCameraTransform()
    {
        // Copy the target's transform
        transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
        
        transform.Rotate(rotation);
        
        // Translate by the offset *after* setting the rotation -> translation takes rotation into account
        transform.Translate(-offset);
    }
}