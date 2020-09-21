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
        if (Input.touchCount > 0) { 
            var touch = Input.GetTouch(0); 
            if (touch.phase == TouchPhase.Ended) { 
                if (Input.touchCount == 1) {
                   
                    //Raycast Planes 
                    if(arRaycastManager.Raycast(touch.position, arRaycastHits)) { 
                        var pose = arRaycastHits[0].pose; 
                        CreateCube(pose.position); 
                        return; 
                    }

                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit)) { 
                        if (hit.collider.tag == "cube") { 
                            DeleteCube(hit.collider.gameObject); 
                        } 
                    }

                } 
            } 
        }
    }
    private void CreateCube(Vector3 position) { 
        Instantiate(cubePrefab, position, Quaternion.identity); }

    private void DeleteCube(GameObject cubeObject) { 
        Destroy(cubeObject); }
}
