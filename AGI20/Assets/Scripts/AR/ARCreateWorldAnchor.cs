using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARPointCloudManager))]
public class ARCreateWorldAnchor : MonoBehaviour
{
    public GameObject groundObject;
    public GameObject playerObject;
    public GameObject spiritObject;
    private ARPreviewScript cursorIndicator;
    private ARRaycastManager arRaycastManager;
    private ARReferencePointManager arReferencePointManager;
    private ARPointCloudManager arPointCloudManager;
    private Grid grid;

    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool worldPlaced = false;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        cursorIndicator = FindObjectOfType<ARPreviewScript>();
        arRaycastManager = GetComponent<ARRaycastManager>();
        arReferencePointManager = GetComponent<ARReferencePointManager>();
        arPointCloudManager = GetComponent<ARPointCloudManager>();
    }

    private void Update()
    {
        if (worldPlaced)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began) // make sure we only do this for first touch
            return;

        if (arRaycastManager.Raycast(touch.position, hits, TrackableType.FeaturePoint))
        {
            Pose hitPose = hits[0].pose;
            hitPose.position = grid.GetNearestPointOnGrid(hitPose.position);
            hitPose.rotation = Quaternion.Euler(0, 0, 0);
            ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPose);

            if (referencePoint == null)
            {
                Debug.Log("Something went wrong?");
            }
            else
            {
                referencePoints.Add(referencePoint);
                GameObject obj1 = Instantiate(groundObject, hitPose.position, hitPose.rotation);
                GameObject obj2 = Instantiate(playerObject, hitPose.position, hitPose.rotation);
                GameObject obj3 = Instantiate(spiritObject, hitPose.position, hitPose.rotation);
                worldPlaced = true;
            }
        }
    }
}