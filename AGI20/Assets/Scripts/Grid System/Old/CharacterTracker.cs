using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        //SetCubeNear(new Vector3(1f, 0, 0));
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
             Debug.Log("back");
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.back + Vector3.up ) && GroundTracker.ground.ContainsKey(transform.position + Vector3.back))
                return;
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.back)) {
                SetCubeNear(transform.position + Vector3.back + Vector3.up);
                return;
            }
            SetCubeNear(transform.position + Vector3.back);            
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            Debug.Log("forward");
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.forward + Vector3.up ) && GroundTracker.ground.ContainsKey(transform.position + Vector3.forward))
                return;
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.forward)) {
                SetCubeNear(transform.position + Vector3.forward + Vector3.up);
                return;
            }
            SetCubeNear(transform.position + Vector3.forward);

        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)) {
            Debug.Log("Left");
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.left + Vector3.up ) && GroundTracker.ground.ContainsKey(transform.position + Vector3.left))
                return;
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.left)) {
                SetCubeNear(transform.position + Vector3.left + Vector3.up);
                return;
            }
            SetCubeNear(transform.position + Vector3.left);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("Right");
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.right + Vector3.up ) && GroundTracker.ground.ContainsKey(transform.position + Vector3.right))
                return;
            if (GroundTracker.ground.ContainsKey(transform.position + Vector3.right)) {
                SetCubeNear(transform.position + Vector3.right + Vector3.up);
                return;
            }
            SetCubeNear(transform.position + Vector3.right);
        }

        if (!GroundTracker.ground.ContainsKey(transform.position + Vector3.down)) {
             SetCubeNear(transform.position + Vector3.down);
        }
    }

    private void SetCubeNear(Vector3 nearPoint)
    {
        var Pos = grid.GetNearestPointOnGrid(nearPoint);
        transform.position = Pos;
    }
}
