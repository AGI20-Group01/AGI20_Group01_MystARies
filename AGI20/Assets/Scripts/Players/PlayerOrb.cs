using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class PlayerOrb : MonoBehaviour
{

    public NetworkClient networkClient;
    ARCameraManager arCamera;
    private Vector3 prePos;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = FindObjectOfType<ARCameraManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = arCamera.transform.position;
        newPos.x = Mathf.Round(newPos.x * 1000) / 1000; 
        newPos.y = Mathf.Round(newPos.y * 1000) / 1000;
        newPos.z = Mathf.Round(newPos.z * 1000) / 1000;

        if (prePos != newPos) {
            networkClient.sendPlayerPos(newPos);
        }
        prePos = newPos;
    }
}
