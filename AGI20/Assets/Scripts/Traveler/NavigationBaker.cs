using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    public GroundTracker ground;
    public GameObject cubePrefab;
    public Transform theGround;

    //public static Dictionary<Vector3, GameObject> ground = new Dictionary<Vector3, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        BakeSurface();
    }

    public void BakeSurface()
    {
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    public void TestPlace()
    {
        //ground.AddCube(new Vector3(0, 0, 3), 0);
       GameObject go = Instantiate(cubePrefab, new Vector3(0, 0, 3), Quaternion.identity);
        go.transform.SetParent(theGround);
        NavMeshSurface nm = go.GetComponent<NavMeshSurface>();
        surfaces.Add(nm);
        
        BakeSurface();
    }
    public void AddToSurface(GameObject go)
    {
        NavMeshSurface nm = go.GetComponent<NavMeshSurface>();
        surfaces.Add( nm);
    }

}
