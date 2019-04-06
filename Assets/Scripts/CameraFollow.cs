using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;  // The position that that camera will be following.

    private Vector3 offset;  // The initial offset from the target.

    void Start()
    {
       offset = target.transform.position - transform.position;
    }

    void Update()
    {
       SetCameraTransform();
    }

    void SetCameraTransform()
    {
        // Copy the target's transform
        transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
        
        // Translate by the offset *after* setting the rotation -> translation takes rotation into account
        transform.Translate(-offset);
    }
}