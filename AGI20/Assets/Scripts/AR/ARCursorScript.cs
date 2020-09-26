using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCursorScript : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject cursor;

    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        cursor = transform.GetChild(0).gameObject;

        cursor.SetActive(false);
    }

    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!cursor.activeInHierarchy)
                cursor.SetActive(true);
        }
    }
}
