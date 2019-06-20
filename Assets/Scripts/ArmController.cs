using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    private Transform rotor;
    private Transform extender;

    // These values will be added to the externally provided transform params
    private float baseRadius = 5.0f;
    private float baseXRotation = 30f;
    private float baseYRotation = 0f;

    // Limits
    private float xRotationLowerBound = 10f;
    private float xRotationUpperBound = 90f;

    void Start()
    {
        rotor = transform.Find("ArmRotor");
        extender = rotor.Find("ArmExtender");
        Reset();
    }

    public void Reset()
    {
        SetRadius(baseRadius);
        SetRotation(baseXRotation, baseYRotation);
    }

    private void SetRadius(float radius)
    {
        Vector3 position = new Vector3(0, 0, -radius);
        extender.localPosition = position;
    }

    private void SetRotation(float xRotation, float yRotation)
    {
        Quaternion rotation = Quaternion.Euler(
            Mathf.Clamp(
                xRotation,
                xRotationLowerBound,
                xRotationUpperBound
            ),
            yRotation,
            0);
        rotor.localRotation = rotation;
    }

    public void SetExtraRotation(float xRotation, float yRotation)
    {
        SetRotation(xRotation + baseXRotation, yRotation + baseYRotation);
    }

    public void Rotate(float xRotation, float yRotation)
    {
        Vector3 currentRotation = rotor.localRotation.eulerAngles;
        SetRotation(xRotation + currentRotation.x, yRotation + currentRotation.y);
    }

    public void SmoothlyResetRotation()
    {
        Vector3 currentRotation = rotor.localRotation.eulerAngles;
        SetRotation(
            Mathf.LerpAngle(currentRotation.x, baseXRotation, Time.deltaTime),
            Mathf.LerpAngle(currentRotation.y, baseYRotation, Time.deltaTime)
        );
    }
}