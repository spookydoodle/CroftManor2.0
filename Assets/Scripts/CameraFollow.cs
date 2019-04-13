using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public GameObject target;  // The position that that camera will be following.
    public bool invertYaxis = false;

    private float smoothing = 1f;
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
        // HandleRotation(setInvYax * y * Settings.MouseSensitivity());
        SetCameraTransform();
    }

    void HandleRotation(float rotationValue)
    {
        float rotationX = rotationValue * rotationSpeed;
        rotation.x = Mathf.Clamp(rotationX, -15f, 50f);
    }

    void SetCameraTransform()
    {
	Quaternion targetRotation = target.transform.rotation;
	Quaternion baseRotation = transform.rotation;
	float lerp = Time.deltaTime * smoothing;

        transform.SetPositionAndRotation(target.transform.position, Quaternion.Lerp(baseRotation, targetRotation, lerp));
        
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
