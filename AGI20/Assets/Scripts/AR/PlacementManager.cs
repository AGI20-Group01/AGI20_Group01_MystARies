using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour
{

    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    public GameObject PointerObj;
    public ARCreateWorldAnchor WorldAnchor;
    private bool PlacingState;
    public GameObject CubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        WorldAnchor = FindObjectOfType<ARCreateWorldAnchor>();
        PointerObj = transform.GetChild(0).gameObject;
        

        PointerObj.SetActive(false);
        PlacingState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlacingState)
        {
            arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), arRaycastHits, TrackableType.Planes);
            if (arRaycastHits.Count > 0)
            {
                transform.position = arRaycastHits[0].pose.position;
                transform.rotation = arRaycastHits[0].pose.rotation;
                if (!PointerObj.activeInHierarchy) { PointerObj.SetActive(true); }
            }
        }
        
    }

    public void PlaceAnchor()
    {
        Instantiate(CubePrefab, PointerObj.transform.position, PointerObj.transform.rotation);
    }
}
