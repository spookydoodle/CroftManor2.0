using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;            // The position that that camera will be following.
    Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        // Calculate the initial offset.
        offset = target.transform.position - transform.position;
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
        float angle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.rotation = target.transform.rotation * Quaternion.Euler(5, 0, 0);
        //transform.position = target.transform.position - offset;
        //transform.LookAt(target.transform);
    }
}
