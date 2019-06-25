using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public ButlerPovController target;

    private bool shouldFollowPosition = true;
    private bool shouldFollowRotation = true;
    private float positionSmoothing = 5f;
    private float rotationSmoothing = 1.5f;

    void Update()
    {
        if (shouldFollowPosition)
        {
            FollowPosition();
        }

        if (shouldFollowRotation)
        {
            FollowRotation();
        }
    }

    void FollowPosition()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position, positionSmoothing * Time.deltaTime);
    }

    void FollowRotation()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target.transform.rotation, rotationSmoothing * Time.deltaTime);
    }

    public void SetFollowPosition(bool value)
    {
        this.shouldFollowPosition = value;
    }

    public void SetFollowRotation(bool value)
    {
        this.shouldFollowRotation = value;
    }
}