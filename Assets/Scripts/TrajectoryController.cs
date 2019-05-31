using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    private LineRenderer renderer = null;
    private static int nodesCount = 100;
    private static int projectionLength = 10;


    void Start()
    {
        this.renderer = this.gameObject.GetComponent<LineRenderer>();

        // TODO: actually compute the positions based on gravity
        Vector3 [] positions = (
            from i in Enumerable.Range(0, nodesCount)
            select new Vector3(0, 2 * Mathf.Sin(2f * Mathf.PI * i / (float)nodesCount), (float)i / projectionLength)
        ).ToArray();
        
        this.renderer.positionCount = positions.Length;
        this.renderer.SetPositions(positions);
    }
}
