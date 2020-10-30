using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTracker : MonoBehaviour
{
    public static Dictionary<Vector3, GameObject> ground = new Dictionary<Vector3, GameObject>();
    private Grid grid;
    public GameObject[] groundTypes;
    public Transform theGround;



    void Start() {
        grid = FindObjectOfType<Grid>();
        snapAllOnjects(theGround, ground);
    }

    public void snapAllOnjects() { 
        grid = FindObjectOfType<Grid>();
        Dictionary<Vector3, GameObject> ground = new Dictionary<Vector3, GameObject>();
        snapAllOnjects(theGround, ground);
    }
    
    private void snapAllOnjects(Transform parent, Dictionary<Vector3, GameObject> ground) {
        foreach(Transform child in parent) {
            if (child.tag != "GroundeCube" && child.tag != "interactablecube") {
                snapAllOnjects(child, ground);
            } else {
                Vector3 pos = child.position;
                pos = grid.GetNearestPointOnGrid(pos);
                if (ground.ContainsKey(pos)) {
                    DestroyImmediate(child.gameObject);
                    return;
                }
                updateCubesSurrounding(child.gameObject, pos, ground);

                ground.Add(pos, child.gameObject);
                child.position = pos;
            }
        }
    }

    private void updateCubesSurrounding(GameObject go, Vector3 pos) {
        updateCubesSurrounding(go, pos, ground);
    }

    private void updateCubesSurrounding(GameObject go, Vector3 pos, Dictionary<Vector3, GameObject> ground) {
        GroundCube gc = go.GetComponent<GroundCube>();


        gc.SetUnder(false, 0);
        gc.SetUnder(false, 1);
        gc.SetUnder(false, 2);
        gc.SetUnder(false, 3);

        if (ground.ContainsKey(pos + new Vector3(0,1,0))) {
            gc.SetUnder(true, 0);
        }
        if (ground.ContainsKey(pos + new Vector3(0,-1,0))) {
            gc.SetUnder(true, 2);
        }
        if (ground.ContainsKey(pos + new Vector3(1,0,0))) {
            gc.SetUnder(true, 1);
        }
        if (ground.ContainsKey(pos + new Vector3(-1,0,0))) {
            gc.SetUnder(true, 3);
        } 


        if (ground.ContainsKey(pos + new Vector3(0,-1,0))) {
            ground[(pos + new Vector3(0,-1,0))].GetComponent<GroundCube>().SetUnder(true, 0);
        }
        if (ground.ContainsKey(pos + new Vector3(0,1,0))) {
            ground[(pos + new Vector3(0,1,0))].GetComponent<GroundCube>().SetUnder(true, 2);
        }
        if (ground.ContainsKey(pos + new Vector3(-1,0,0))) {
            ground[(pos + new Vector3(-1,0,0))].GetComponent<GroundCube>().SetUnder(true, 1);
        }
        if (ground.ContainsKey(pos + new Vector3(1,0,0))) {
            ground[(pos + new Vector3(1,0,0))].GetComponent<GroundCube>().SetUnder(true, 3);
        }
    }

    public void AddCube(Vector3 pos, int type) {
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);

        if (ground.ContainsKey(gridPos)) {
            return;
        }
        GameObject go = Instantiate(groundTypes[type], gridPos, theGround.rotation);
        updateCubesSurrounding(go, grid.GetNearestPointOnGrid(Quaternion.Inverse(transform.rotation) * gridPos));
        ground.Add(grid.GetNearestPointOnGrid(Quaternion.Inverse(transform.rotation) * gridPos), go);
        go.transform.SetParent(theGround);
    }

    public void RemoveCube(Vector3 pos) {
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);

        if (ground.ContainsKey(gridPos)) {
            Destroy(ground[gridPos]);

            ground.Remove(transform.rotation * gridPos);

            if (ground.ContainsKey(pos + new Vector3(0,-1,0))) {
                ground[(pos + new Vector3(0,-1,0))].GetComponent<GroundCube>().SetUnder(false, 0);
            }
            if (ground.ContainsKey(pos + new Vector3(0,1,0))) {
                ground[(pos + new Vector3(0,1,0))].GetComponent<GroundCube>().SetUnder(false, 2);
            }
            if (ground.ContainsKey(pos + new Vector3(-1,0,0))) {
                ground[(pos + new Vector3(-1,0,0))].GetComponent<GroundCube>().SetUnder(false, 1);
            }
            if (ground.ContainsKey(pos + new Vector3(1,0,0))) {
                ground[(pos + new Vector3(1,0,0))].GetComponent<GroundCube>().SetUnder(false, 3);
            }
        }
    }

}