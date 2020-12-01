﻿using System.Collections;
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
    public GameObject levelPrefab;
    private GroundTracker groundTracker;

    private ARReferencePointManager arReferencePointManager;
    private ARPointCloudManager arPointCloudManager;
    private ARPlaneManager arPlaneManager;
    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();
    public GameObject placeWorldButton;


    // Placement Manager 
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject PointerObj;
    private bool PlacingState;

    public GameObject worldButton;
    public GameObject arInfo;




    // Start is called before the first frame update
    void Start()
    {
        arReferencePointManager = FindObjectOfType<ARReferencePointManager>();
        arPointCloudManager = FindObjectOfType<ARPointCloudManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();

        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        PointerObj = transform.GetChild(0).gameObject;
        PlacingState = true;

        PointerObj.SetActive(false);

        worldButton.gameObject.SetActive(false);
        arInfo.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlacingState)
        {
            arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                arInfo.gameObject.SetActive(false);
                worldButton.gameObject.SetActive(true);

                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                if (!PointerObj.activeInHierarchy) { PointerObj.SetActive(true); }
            } else {
                PointerObj.SetActive(false);
                worldButton.gameObject.SetActive(false);
                arInfo.gameObject.SetActive(true);
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
                Pose hitPose = hits[0].pose;
                hitPose.rotation = Quaternion.Euler(0, 0, 0);
                ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPose);

                if (referencePoint != null)
                {
                    referencePoints.Add(referencePoint);
                    GameObject obj1 = Instantiate(levelPrefab, hitPose.position, hitPose.rotation);
                    groundTracker = FindObjectOfType <GroundTracker>();
                    groundTracker.snapAllOnjects();
                    PlacingState = false;
                    PointerObj.SetActive(false);
                    placeWorldButton.SetActive(false); 

                }
            }
        }
    }
}
