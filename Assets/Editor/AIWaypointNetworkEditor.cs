using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

// Associate script with component
[CustomEditor(typeof(AIWaypointNetwork))]
public class AIWayPointNetworkEditor : Editor
{
    // Below function overwrites the default ui controls in inspector panel
    public override void OnInspectorGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        // Cast to PathDisplayMode ???
        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup("Display Mode", network.DisplayMode);

        if (network.DisplayMode == PathDisplayMode.Paths)
        {
            network.UIStart = EditorGUILayout.IntSlider("Waypoint Start", network.UIStart, 0, network.Waypoints.Capacity + 1);
            network.UIEnd = EditorGUILayout.IntSlider("Waypoint Start", network.UIEnd, 0, network.Waypoints.Capacity + 1);
        }


        // Below method worksin combination with [HideInInspector] in AIWayPointNetwork.cs
        DrawDefaultInspector();
    }

    // Lookup Unity documentation - Editor
    void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        for (int i = 0; i < network.Waypoints.Count; i++)
        {
            // Lookup documentation for Handles
            if (network.Waypoints[i] != null)
                Handles.color = Color.cyan;
            Handles.Label(network.Waypoints[i].position, "Waypoint " + i.ToString());
        }

        if (network.DisplayMode == PathDisplayMode.Connections)
        {
            Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

            for (int i = 0; i <= network.Waypoints.Count; i++)
            {
                int index = i != network.Waypoints.Count ? i : 0;
                if (network.Waypoints[index] != null)
                    linePoints[i] = network.Waypoints[index].position;
                else
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
            }

            Handles.color = Color.cyan;
            Handles.DrawPolyLine(linePoints);
        }
        else
        {
            if (network.DisplayMode == PathDisplayMode.Paths)
            {
                NavMeshPath path = new NavMeshPath();

                if (network.Waypoints[network.UIStart] != null && network.Waypoints[network.UIEnd] != null)
                {
                    Vector3 from = network.Waypoints[network.UIStart].position;
                    Vector3 to = network.Waypoints[network.UIEnd].position;

                    // Look up NavMesh in documentation
                    NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
                    Handles.color = Color.yellow;
                    Handles.DrawPolyLine(path.corners);
                }

            }
        }

    }
}
