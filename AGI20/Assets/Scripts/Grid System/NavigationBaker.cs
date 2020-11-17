using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;


public class NavigationBaker : MonoBehaviour
{
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

    //  Variables for testing
   // public GroundTracker groundTracker;
   //public GameObject cubePrefab;
    //public GameObject navMeshSurfObj;
    //public Transform groundParent;
   // public NavMeshSurface nm;
   // private Vector3 v = new Vector3(0, 0, 3);

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
    /*
    public void TestPlace()
    {
        groundTracker.AddCube(v, 0);
        v = v + new Vector3(1, 0, 3);
    }*/

    public void AddToSurface(/*GameObject go*/)
    {
        //NavMeshSurface nm = go.GetComponent<NavMeshSurface>();
        //surfaces.Add( nm);
        BakeSurface();
    }
    

}
