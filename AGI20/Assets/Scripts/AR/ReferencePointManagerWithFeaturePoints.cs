using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARPointCloudManager))]
public class ReferencePointManagerWithFeaturePoints : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private ARReferencePointManager arReferencePointManager;
    private ARPointCloudManager arPointCloudManager;

    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public static Vector3 refPoint;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arReferencePointManager = GetComponent<ARReferencePointManager>();
        arPointCloudManager = GetComponent<ARPointCloudManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began) // make sure we only do this for first touch
            return;

        if(arRaycastManager.Raycast(touch.position, hits, TrackableType.FeaturePoint))
        {
            Pose hitPose = hits[0].pose;
            ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPose);

            refPoint = hitPose.position;

            if (referencePoint == null)
            {
                Debug.Log("Something went wrong?");
            }
            else
            {
                referencePoints.Add(referencePoint);
            }
        }
    }
}
