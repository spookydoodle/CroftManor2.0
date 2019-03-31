using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;            // The position that that camera will be following.
    Vector3 offset;                     // The initial offset from the target.\
    float mouseX, mouseY;
    public float RotationSpeed = 1.0f;

    void Start()
    {
        // Calculate the initial offset.
        offset = target.transform.position - transform.position;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    //void FixedUpdate()
    //{
    //    // Create a postion the camera is aiming for based on the offset from the target.
    //    Vector3 targetCamPos = target.position + offset;

    //    // Smoothly interpolate between the camera's current position and it's target position.
    //    transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    //}

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        float angle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, angle+mouseX, 0);
        Quaternion camRotation = Quaternion.Euler(mouseY, mouseX, 0);

        transform.LookAt(target.transform);


        transform.position = target.transform.position - (rotation * offset);
        transform.rotation = target.transform.rotation * camRotation;
        //transform.position = target.transform.position - offset;
        //target.transform.rotation = Quaternion.Euler(0, mouseX, 0);

        //CamControl();
    }


    void CamControl()
    {
        
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target.transform);

        target.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //player.transform.rotation = Quaternion.Euler(0, mouseX, 0);

    }
}
