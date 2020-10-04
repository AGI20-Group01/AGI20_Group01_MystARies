using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARPointCloudManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARCreateWorldAnchor : MonoBehaviour
{
    public GameObject groundObject;
    public GameObject playerObject;
    public GameObject spiritObject;

    private ARPreviewScript cursorIndicator;
    private ARRaycastManager arRaycastManager;
    private ARReferencePointManager arReferencePointManager;
    private ARPointCloudManager arPointCloudManager;
    private ARPlaneManager arPlaneManager;

    private Grid grid;

    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public bool worldPlaced = false;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        cursorIndicator = FindObjectOfType<ARPreviewScript>();
        arRaycastManager = GetComponent<ARRaycastManager>();
        arReferencePointManager = GetComponent<ARReferencePointManager>();
        arPointCloudManager = GetComponent<ARPointCloudManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    private void Update()
    {
        if (worldPlaced) { 
            return;
        }

        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        /*
        if (touch.phase != TouchPhase.Began) // make sure we only do this for first touch
        { return; }
        */
        if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose;
            hitPose.position = grid.GetNearestPointOnGrid(hitPose.position);
            hitPose.rotation = Quaternion.Euler(0, 0, 0);
            ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPose);

            //arPlaneManager.GetPlane(ARRaycastHit.trackableId.ToString);

            if (referencePoint == null)
            {
                Debug.Log("Something went wrong?");
            }
            else
            {
                referencePoints.Add(referencePoint);
                GameObject obj1 = Instantiate(groundObject, hitPose.position, hitPose.rotation);
                GameObject obj2 = Instantiate(playerObject, hitPose.position, hitPose.rotation);
                spiritObject.SetActive(true);
                worldPlaced = true;
            }
        }
    }
}