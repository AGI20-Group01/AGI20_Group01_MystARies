using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARControle : MonoBehaviour
{

    public GameObject placementIndicator;
    public GameObject cube;

    private ARRaycastManager arRaycaster;
    private Pose placementPose;
    private bool placementPoseValid = false;

    // Start is called before the first frame update
    void Start()
    {
        arRaycaster = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseValid && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(cube, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        placementPoseValid = arRaycaster.Raycast(screenCenter, hits);
        float closesDist = 101;
    
        if (placementPoseValid)
        {

            closesDist = hits[0].distance;
            placementPose = hits[0].pose;
        }

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.distance < closesDist)
            {
                Pose hitPose = new Pose();
                hitPose.position = hit.point;
                hitPose.rotation = Quaternion.LookRotation (-hit.normal, Vector3.up); 
                placementPose = hitPose;
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            return;
        }
        placementIndicator.SetActive(false);

    }
}
