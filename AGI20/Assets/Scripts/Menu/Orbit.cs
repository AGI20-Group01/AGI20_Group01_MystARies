using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{/* the object to orbit */
    public Transform target;
    public Vector3 axis;

    /* speed of orbit (in degrees/second) */
    public float speed;

    public void Update()
    {
        if (target != null)
        {
            transform.RotateAround(target.position, axis, speed * Time.deltaTime);
        }
    }
}
