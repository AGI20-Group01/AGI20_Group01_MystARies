using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyCube : MonoBehaviour
{

    private RotationHandler rotationHandler;


    // Start is called before the first frame update
    void Start()
    {
        rotationHandler = GameObject.FindGameObjectWithTag("GridHandler").GetComponent<RotationHandler>();
        //rotationHandler.setTargetRot(new Vector3(0,0,1));

    }

    /*void Update()
    {
        transform.rotation = Quaternion.identity;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trig");
        rotationHandler.setTargetRot(new Vector3(0,0,1));
    }
}
