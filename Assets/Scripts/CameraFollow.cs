using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public GameObject target;  // The position that that camera will be following.
    public bool invertYaxis = false;

    private float rotationSpeed = 1f;
    private Vector3 offset;  // The initial offset from the target.
    private Vector3 rotation = Vector3.zero;
    private int setInvYax;

    void Start()
    {
        offset = target.transform.position - transform.position;
        handleSettings(invertYaxis);
    }

    void Update()
    {
        float y = Input.GetAxis("Mouse Y");
        HandleRotation(setInvYax * y * Settings.MouseSensitivity());
        SetCameraTransform();
    }

    void HandleRotation(float rotationValue)
    {
        rotation.x += rotationValue * rotationSpeed;

        // Limit the rotation to <-45: Down, 90: Up>
        rotation.x = Mathf.Clamp(rotation.x, -15f, 50f);
        Debug.Log(rotation.x);
    }

    void SetCameraTransform()
    {
        // Copy the target's transform
        transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
        
        transform.Rotate(rotation);
        
        // Translate by the offset *after* setting the rotation -> translation takes rotation into account
        transform.Translate(-offset);
    }

    void handleSettings(bool invertYaxis)
    {
        if (invertYaxis)
            setInvYax = -1;
        else
            setInvYax = 1;
    }
}