using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCube : MonoBehaviour
{
    [SerializeField, SerializeReference]
    public CubeMaterialCreater materialCreater;
    private static int nextID = 1;
    public int id = 0;

    public Transform cubeCollider;

    private bool[] surroundingGrund = new bool[4];

    private MeshRenderer meshRenderer = null;


    void Start()
    {
        id = nextID;
        nextID++;
    }

    void Update()
    {
        if (cubeCollider) {
            cubeCollider.rotation = Quaternion.identity;
        }
    }


    public void SetUnder(bool start, int pos) {
        surroundingGrund[pos] = start;
        updateMaterial();
    }


    private void updateMaterial() {
        if (!materialCreater) {
            return;
        }
        if (meshRenderer == null) {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        int val = (surroundingGrund[0]) ? 1 : 0;
        val += 2 * ((surroundingGrund[1]) ? 1 : 0);
        val += 4 * ((surroundingGrund[2]) ? 1 : 0);
        val += 8 * ((surroundingGrund[3]) ? 1 : 0);
        meshRenderer.material = materialCreater.GetMaterial(val);

    }

}