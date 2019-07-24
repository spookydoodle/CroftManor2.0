using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    public float optimalRadius = baseRadius;

    private Transform rotor;
    private Transform extender;

    // These values will be added to the externally provided transform params
    private static float baseRadius = 5.0f;
    private static float baseXRotation = 0f;
    private static float baseYRotation = 0f;

    // Limits
    private float xRotationLowerBound = 0f;
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

    public void SetRadius(float radius)
    {
        // handle invalid Input
        radius = Mathf.Max(radius, 0.1f);
        Vector3 position = new Vector3(0, 0, -radius);
        extender.localPosition = position;
    }

    public bool IsRadiusOptimal()
    {
        return extender.localPosition.z == optimalRadius;
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