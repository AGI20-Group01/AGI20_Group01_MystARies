using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorPointTracker : MonoBehaviour
{
    private Grid grid;
    public GameObject referencePoint;
    private ARReferencePointManager arReferencePointManager;

    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        Vector3 gridPos = grid.GetNearestPointOnGrid(referencePoint.transform.position);
        Quaternion rot = referencePoint.transform.rotation;

        Pose pose = new Pose(gridPos, rot);
        ARReferencePoint refPoint = arReferencePointManager.AddReferencePoint(pose);
        referencePoints.Add(refPoint);
    }
}