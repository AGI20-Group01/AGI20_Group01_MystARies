using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorHandler : MonoBehaviour
{
    private Grid grid;
    

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        Vector3 pos = this.gameObject.transform.position;
        Vector3 gridPos = grid.GetNearestPointOnGrid(pos);
        this.gameObject.transform.position = gridPos;
    }

}

