using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARPointCloudManager))]
[RequireComponent(typeof(ARPlaneManager))]

public class PlacementManager : MonoBehaviour
{
    // from ARCreateWorldAnchor
    public GameObject groundObject;
    public GameObject playerObject;
    public GameObject spiritObject;

    private ARReferencePointManager arReferencePointManager;
    private ARPointCloudManager arPointCloudManager;
    private ARPlaneManager arPlaneManager;
    private Grid grid;
    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();
    public GameObject ground;
    public GroundTracker groundTracker;


    // Placement Manager 
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject PointerObj;
    public ARCreateWorldAnchor WorldAnchor;
    private bool PlacingState;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        arReferencePointManager = FindObjectOfType<ARReferencePointManager>();
        arPointCloudManager = FindObjectOfType<ARPointCloudManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        groundTracker = FindObjectOfType<GroundTracker>();

        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        WorldAnchor = FindObjectOfType<ARCreateWorldAnchor>();
        PointerObj = transform.GetChild(0).gameObject;
        PlacingState = true;

        PointerObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlacingState)
        {
            arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                if (!PointerObj.activeInHierarchy) { PointerObj.SetActive(true); }
            }
        }  
    }

    public void PlaceAnchor()
    {
        if (PlacingState)
        {
            arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                //PlacingState = WorldAnchor.InstantiateAnchor(arRaycastHits[0].pose);
                Pose hitPose = hits[0].pose;
                hitPose.position = grid.GetNearestPointOnGrid(hitPose.position);
                hitPose.rotation = Quaternion.Euler(0, 0, 0);
                ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPose);

                if (referencePoint != null)
                {
                    referencePoints.Add(referencePoint);
                    GameObject obj1 = Instantiate(groundObject, hitPose.position, hitPose.rotation);
                    obj1.transform.SetParent(ground.transform, false);
                    groundTracker.snapAllOnjects();
                    GameObject obj2 = Instantiate(playerObject, hitPose.position, hitPose.rotation);
                    spiritObject.SetActive(true);
                    PlacingState = false;
                    PointerObj.SetActive(false);

                }
            }
        }
    }
}
