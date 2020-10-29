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
    //public ARCreateWorldAnchor ARCreateWorldAnchor;
    //public GameObject cubePrefab;
   void Start()
    {
       arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
    void OnEnable() {
        /*if (ARCreateWorldAnchor.worldPlaced == true)
        {
            ARCreateWorldAnchor.enabled = false;
        }*/
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

                                    // calculate the rotation to create the object aligned with the face normal:
                                    //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                                    // create the object at the face center, and perpendicular to it:
                                    // GameObject Placement = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                    // Placement.transform.position = position;
                                    //Placement.transform.rotation = rotation;
                        //CreateCube(position);
                    }
                    if (Input.touchCount == 2)
                    {

                        if (hit.collider.tag == "interactablecube")
                        {
                            groundTracker.RemoveCube(hitpos);
                            //DeleteCube(hit.collider.gameObject);
                        }

                    }
                }
            }
        }
    }
    /*
    private void CreateCube(Vector3 position)
    {
        Instantiate(cubePrefab, position, Quaternion.identity);
    }
    
    private void DeleteCube(GameObject cubeObject)
    {
        Destroy(cubeObject);
    }
    */
    
}
