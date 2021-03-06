﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class SpiritController : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    public GroundTracker groundTracker;
    [SerializeField]
    private NetworkClient networkClient;


    private float holdTime = 0.8f; //or whatever
    private float acumTime = 0;
    private Vector3 heldPos;

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        networkClient = FindObjectOfType<NetworkClient>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            acumTime += touch.deltaTime;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Hold(hit);
                        }
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        if(acumTime > holdTime) RDG.Vibration.Vibrate(new long[] { 5, 10, 5, 10 }, new int[] { 0, 255, 0, 100 }, -1, RDG.Vibration.CancelVibration);
                        break;
                    }
                case TouchPhase.Stationary:
                    {
                        if(acumTime > holdTime) RDG.Vibration.Vibrate(new long[] { 5, 10, 5, 10 }, new int[] { 0, 255, 0, 100 }, -1, RDG.Vibration.CancelVibration);
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Hit(hit);
                        }
                        break;
                    }
        }
        
        }
    }

    void TwoTap(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitpos = hit.transform.position;
            if (Input.touchCount == 1)
            {
                Vector3 position = hitpos + hit.normal;

                groundTracker.AddCube(position, 0);
                networkClient.snedAddCube(position);
            }
            if (Input.touchCount == 2)
            {

                if (hit.collider.tag == "interactablecube")
                {
                    groundTracker.RemoveCube(hitpos);
                    networkClient.snedRemoveCube(hitpos);
                    //DeleteCube(hit.collider.gameObject);
                }
                else
                {
                    groundTracker.ShakeCube(hitpos);
                }

            }
        }
    }



    void Pinch()
    {

        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitpos = hit.transform.position;
                    Vector3 position = hitpos + hit.normal;

                    groundTracker.AddCube(position, 0);
                    networkClient.snedAddCube(position);
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touchzero = Input.touches[0];
            Touch touchone = Input.touches[1];

            Vector3 zeroPrevPos = touchzero.position - touchzero.deltaPosition;
            Vector3 onePrevPos = touchone.position - touchone.deltaPosition;

            float prevMagnitude = (zeroPrevPos - onePrevPos).magnitude;
            float currMagnitude = (touchzero.position - touchone.position).magnitude;
            // potentiellt: kolla att currmagnitude är väldigt liten, aka touch väldigt nära varandra

            if (currMagnitude < prevMagnitude)
            {
                Ray rayzero = Camera.main.ScreenPointToRay(touchzero.position);
                RaycastHit hitzero;
                Ray rayone = Camera.main.ScreenPointToRay(touchone.position);
                RaycastHit hitone;

                if (Physics.Raycast(rayone, out hitone) && Physics.Raycast(rayzero, out hitzero))
                {
                    if (hitone.collider.tag == "interactablecube" && hitzero.collider.tag == "interactablecube")
                    {
                        GameObject one = hitone.transform.parent.gameObject;
                        GameObject zero = hitzero.transform.parent.gameObject;
                        if (one.GetInstanceID() == zero.GetInstanceID())
                        {
                            Vector3 hitpos = hitone.transform.position;
                            Vector3 position = hitpos + hitone.normal;

                            groundTracker.AddCube(position, 0);
                            networkClient.snedAddCube(position);
                        }
                    }
                }
            }
        }
    }


    void Hold(RaycastHit hit)
    {
            Vector3 hitpos = hit.transform.position;
            heldPos = hitpos;
            groundTracker.Holding(hitpos);
        
      
    }

    void Hit(RaycastHit hit)
    {

        Vector3 hitpos = hit.transform.position;
        groundTracker.Release(heldPos);
       
        if (acumTime < holdTime)
        {
            Vector3 position = hitpos + hit.normal;

            groundTracker.AddCube(position, 0);
            networkClient.snedAddCube(position);
            Debug.Log("Add");
            RDG.Vibration.VibratePredefined(RDG.Vibration.PredefinedEffect.EFFECT_TICK, RDG.Vibration.CancelVibration);
        }
        
        else if (hit.collider.tag == "interactablecube")
        {
            groundTracker.RemoveCube(hitpos);
            Debug.Log("Break");
        }
        else if(hit.collider.tag == "unbreakable")
        {
            groundTracker.ShakeCube(hitpos);
            Debug.Log("unbreakble");
            RDG.Vibration.VibratePredefined(RDG.Vibration.PredefinedEffect.EFFECT_HEAVY_CLICK, RDG.Vibration.CancelVibration);
        }
        else
        {
            Debug.Log("Something is wrong");
        }

        acumTime = 0;
    } 

    public void TestBreak()
    {
        Vector3 pos = new Vector3(0, 0, 2);
        //Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.forward, out hit))
        {
            Debug.Log("hit");
            Hit(hit);
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}


