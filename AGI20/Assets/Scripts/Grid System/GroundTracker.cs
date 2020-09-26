using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<Vector3, GameObject> ground = new Dictionary<Vector3, GameObject>();
    private Grid grid;
    public GameObject[] groundTypes;
    public Transform theGround;


    /*public Slider x;
    public Slider y;
    public Slider z;
    
    public Slider type;
    public Slider op;
    */


    void Start() {
        grid = FindObjectOfType<Grid>();
        int id = 1;
        foreach(Transform child in theGround) {
            Vector3 pos = child.position;
            pos = grid.GetNearestPointOnGrid(pos);
            if (ground.ContainsKey(pos)) {
                Destroy(child.gameObject);
                return;
            }
            updateCubesSurrounding(child.gameObject, pos);

            ground.Add(pos, child.gameObject);
            child.position = pos;
            id++;
        }
    }

    private void updateCubesSurrounding(GameObject go, Vector3 pos) {
        GroundCube gc = go.GetComponent<GroundCube>();
        if (ground.ContainsKey(pos + new Vector3(0,1,0)))
                gc.SetUnder(true);

        if (ground.ContainsKey(pos + new Vector3(0,-1,0)))
            ground[(pos + new Vector3(0,-1,0))].GetComponent<GroundCube>().SetUnder(true);

    }
    //*****
    public void AddCube(Vector3 pos, int type) {
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);

        if (ground.ContainsKey(gridPos)) {
            return;
        }
        GameObject go = Instantiate(groundTypes[type], gridPos, Quaternion.identity);
        updateCubesSurrounding(go, gridPos);
        ground.Add(gridPos, go);
    }

    public void RemoveCube(Vector3 pos) {
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);

        if (ground.ContainsKey(gridPos)) {
            Destroy(ground[gridPos]);
            ground.Remove(gridPos);
            if (ground.ContainsKey(gridPos + new Vector3(0,-1,0)))
                ground[(gridPos + new Vector3(0,-1,0))].GetComponent<GroundCube>().SetUnder(false);
        }
    }
    //******

    /*public void test() {
        float fx = x.value;
        float fy = y.value;
        float fz = z.value;
        int ft = (int) type.value;
        float fop = (int) op.value;

        if (fop == 1)
            AddCube(new Vector3(fx,fy,fz), ft);
        else
            RemoveCube(new Vector3(fx,fy,fz));
    }

    private void OnDrawGizmos()
    {
        float fx = x.value;
        float fy = y.value;
        float fz = z.value;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(fx, fy, fz), 0.1f);

    }*/
}
