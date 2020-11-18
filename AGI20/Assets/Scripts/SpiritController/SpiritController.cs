using System.Collections;
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
            if (touch.phase == TouchPhase.Ended)
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
        }
    }

    void Test()
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
                        if( one.GetInstanceID() == zero.GetInstanceID())
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
    

}


