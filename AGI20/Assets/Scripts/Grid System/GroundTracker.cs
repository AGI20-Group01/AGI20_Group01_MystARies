using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GroundTracker : MonoBehaviour
{
    public static Dictionary<Vector3, GameObject> ground = new Dictionary<Vector3, GameObject>();
    private Grid grid;
    public GameObject[] groundTypes;
    public Transform theGround;
    public NavigationBaker navBaker;

    void Start() {
        grid = FindObjectOfType<Grid>();
        snapAllOnjects(theGround, ground);
        navBaker = FindObjectOfType<NavigationBaker>();
        
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
                Vector3 gridPos = grid.GetNearestPointOnGrid(pos);
                Vector3 gridIndex = grid.GetGridCellIndex(pos);        

                if (ground.ContainsKey(gridIndex)) {
                    DestroyImmediate(child.gameObject);
                    return;
                }
                updateCubesSurrounding(child.gameObject, gridIndex, ground);

                ground.Add(gridIndex, child.gameObject);
                child.position = gridPos;
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
        Vector3 gridIndex = grid.GetGridCellIndex(pos);        

        if (ground.ContainsKey(gridIndex)) {
            return;
        }
        
        GameObject go = Instantiate(groundTypes[type], gridPos, theGround.rotation);
        go.transform.SetParent(theGround);

        updateCubesSurrounding(go, gridIndex);
        ground.Add(gridIndex, go);

        if (navBaker) {
            navBaker.AddToSurface();
        }   
    }

    public void RemoveCube(Vector3 pos) {
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);
        Vector3 gridIndex = grid.GetGridCellIndex(pos);

        if (ground.ContainsKey(gridIndex)) {
            StartCoroutine(BreakBlock(ground[gridIndex]));  // trigger particle effect
            //Destroy(ground[gridIndex]);

            ground.Remove(gridIndex);

            if (ground.ContainsKey(gridIndex + new Vector3(0,-1,0))) {
                ground[(gridIndex + new Vector3(0,-1,0))].GetComponent<GroundCube>().SetUnder(false, 0);
            }
            if (ground.ContainsKey(gridIndex + new Vector3(0,1,0))) {
                ground[(gridIndex + new Vector3(0,1,0))].GetComponent<GroundCube>().SetUnder(false, 2);
            }
            if (ground.ContainsKey(gridIndex + new Vector3(-1,0,0))) {
                ground[(gridIndex + new Vector3(-1,0,0))].GetComponent<GroundCube>().SetUnder(false, 1);
            }
            if (ground.ContainsKey(gridIndex + new Vector3(1,0,0))) {
                ground[(gridIndex + new Vector3(1,0,0))].GetComponent<GroundCube>().SetUnder(false, 3);
            }
        }
        
        if (navBaker) {
            navBaker.AddToSurface();
        }      
    }

    public void ShakeCube(Vector3 pos)
    {
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);
        if (ground.ContainsKey(gridPos))
        {
            Debug.Log("Found object! " + gridPos);

            GameObject block = ground[gridPos];

            Animator animator = block.GetComponent<Animator>();
            animator.SetTrigger("Shake");
        }
        else
        {
            Debug.Log("Object not in ground"+ gridPos);
        }
    }

        private IEnumerator BreakBlock(GameObject block)
    {
        MeshRenderer mr = block.GetComponentInChildren<MeshRenderer>();
        ParticleSystem particle =  block.GetComponentInChildren<ParticleSystem>();
        BoxCollider bc = block.GetComponent<BoxCollider>();

        particle.Play();
        // un render block
        mr.enabled = false;
        // make it not touchable
        bc.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(block);
    }

    public void TestPlace()
    {
        Vector3 test = new Vector3(0, 0, 3);
        AddCube(test, 0);
    }
    
    public void TestBreak()
    {
        RemoveCube(new Vector3(0, 0, 0));
    }

}