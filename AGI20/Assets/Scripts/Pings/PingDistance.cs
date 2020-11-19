using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PingDistance : MonoBehaviour
{
    private TextMeshPro distanceText;
    public Camera arCameraObject;

    private void Awake()
    {
        distanceText = transform.GetComponent<TextMeshPro>();
        arCameraObject = transform.GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 pingPos = transform.parent.position;
        Vector3 cameraPos = arCameraObject.transform.position;
        int distance = Mathf.RoundToInt(Vector3.Distance(pingPos, cameraPos) / 3f);
        distanceText.text = distance + " m";
    }
}
