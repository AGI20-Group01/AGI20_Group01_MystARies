using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        SetCubeNear(new Vector3(1f, 0, 0));
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow)) {
            Debug.Log("Left");
            SetCubeNear(transform.position + Vector3.left);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("Right");
            SetCubeNear(transform.position + Vector3.right);
        }
    }

    private void SetCubeNear(Vector3 nearPoint)
    {
        var Pos = grid.GetNearestPointOnGrid(nearPoint);
        transform.position = Pos;
    }
}
