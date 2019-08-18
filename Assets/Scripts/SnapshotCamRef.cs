using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotCamRef : MonoBehaviour
{
    public Snapshot snapCam;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            snapCam.CallTakeSnapshot();
        }
    }
}
