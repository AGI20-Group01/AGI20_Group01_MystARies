using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_AnimationExample : MonoBehaviour
{
    public Vector3 destination;
    public GameObject PlayerTracker;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, destination, 5f * Time.deltaTime);
        destination = PlayerTracker.transform.position;
    }
}
