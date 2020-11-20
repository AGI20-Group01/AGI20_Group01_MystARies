using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{/* the object to orbit */
    public Transform target;
    public Vector3 axis;
    public Vector3 offset;
    private Vector3 center;

    /* speed of orbit (in degrees/second) */
    public float speed;

    public void Update()
    {
        if (target != null)
        {
            center = new Vector3(0,0,0);
            center.y += offset.y;
            transform.RotateAround(target.position + center, axis, speed * Time.deltaTime);
        }
    }

    
}
