using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    public Vector3? initialSpeed = null;
    public bool withGravity = false;

    private LineRenderer renderer = null;
    private static int nodesCount = 100;
    private static int projectionTime = 1;  // project 1 second of movement
    private bool shouldRecalculate = true;  // only recalculate if values have changed

    void Start()
    {
        this.renderer = this.gameObject.GetComponent<LineRenderer>();
    }

    void Update() {
        if (this.shouldRecalculate)
        {
            this.UpdatePositions();
            this.shouldRecalculate = false;
        }
    }

    public void SetWithGravity(bool value)
    {
        this.withGravity = value;
        this.shouldRecalculate = true;
    }

    public void SetInitialSpeed(Vector3 value)
    {
        this.initialSpeed = value;
        this.shouldRecalculate = true;
    }

    private void UpdatePositions()
    {
        var positions = this.CalculatePositions();
        this.renderer.positionCount = positions.Length;
        this.renderer.SetPositions(positions);
    }

    private Vector3[] CalculatePositions()
    {
        if (initialSpeed.HasValue)
        {
            return (
                from i in Enumerable.Range(0, nodesCount)
                select this.TotalDisplacementAtTime(initialSpeed.Value, (float)i * projectionTime / nodesCount)
            ).ToArray();
        }
        else
        {
            return new Vector3[0];
        }
    }

    private Vector3 TotalDisplacementAtTime(Vector3 initialSpeed, float seconds)
    {
        Vector3 accelerations;
        if (this.withGravity)
        {
            accelerations = Physics.gravity;  // assume gravity is the only force
        }
        else
        {
            accelerations = new Vector3(0, 0, 0);
        }

        // x = v0 * t + 1/2 * a * t^2
        return initialSpeed * seconds + 0.5f * accelerations * Mathf.Pow(seconds, 2);
    }
}
