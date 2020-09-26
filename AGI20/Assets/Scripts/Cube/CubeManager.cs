using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;



public class CubeManager : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject cubePrefab;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
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
                if (Input.touchCount == 1)
                {

                    if (Physics.Raycast(ray, out hit))
                    {
                        // it's better to find the center of the face like this:
                        Vector3 position = hit.transform.position + hit.normal;

                        // calculate the rotation to create the object aligned with the face normal:
                        //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                        // create the object at the face center, and perpendicular to it:
                        GameObject Placement = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Placement.transform.position = position;
                        //Placement.transform.rotation = rotation;
                        CreateCube(position);


                        //Instantiate<( PrimitiveType.Cube as GameObject , position , rotation ) as GameObject;
                    }
                }
                if (Input.touchCount == 2)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "cube")
                        {
                            DeleteCube(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
    private void CreateCube(Vector3 position )
    {
        Instantiate(cubePrefab, position, Quaternion.identity);
    }

    private void DeleteCube(GameObject cubeObject)
    {
        Destroy(cubeObject);
    }
}
