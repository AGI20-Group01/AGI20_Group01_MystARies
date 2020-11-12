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
    
}
