using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;



public class CubeManager : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    public GroundTracker ground; 

    // Start is called before the first frame update
    void Start()
    {

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
                    Vector3 position = hit.transform.position + hit.normal;
                    if (hit.collider.tag == "cube")
                    {
                        if (Input.touchCount == 1)
                        {                            
                            ground.AddCube(position,1);
                            // calculate the rotation to create the object aligned with the face normal:
                        }

                        if (Input.touchCount == 2)
                        {
                            ground.RemoveCube(position);
                        }
                    }
                }
            }
        }
    }
   
}
