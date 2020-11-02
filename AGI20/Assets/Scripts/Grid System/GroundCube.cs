using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCube : MonoBehaviour
{

    public Material underMaterial;
    public Material normalMaterial;
    private static int nextID = 1;
    public int id = 0;
    //[SerializeField]
    //private string type = "";

    private bool underGroundCube = false;

    private MeshRenderer meshRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        //meshRenderer = GetComponentInChildren<MeshRenderer>();
        id = nextID;
        nextID++;
    }

    

    public void SetUnder(bool start) {
        underGroundCube = start;
        updateMaterial();
    }


    private void updateMaterial() {
        if (meshRenderer == null) {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }
        if (underGroundCube)
            meshRenderer.material = underMaterial;
        else
            meshRenderer.material = normalMaterial;
    }

    public void setId(int id) {
        this.id = id;
    }
}
