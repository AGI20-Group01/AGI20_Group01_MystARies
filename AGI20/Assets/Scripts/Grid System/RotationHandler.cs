using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    private Vector3 targetRot; 

    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        targetRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        handleInputs();
        //updateRotation();
    }


    void handleInputs() {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            setTargetRot(new Vector3(0,0,1));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            setTargetRot(new Vector3(0,0,-1));
        }

    }


    void updateRotation() {
        Vector3 newRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRot), Time.time * rotSpeed).eulerAngles;
        if (Quaternion.Angle( Quaternion.Euler(targetRot),transform.rotation ) < 0.1) {
            newRot = targetRot;
        }
        Debug.Log(transform.eulerAngles + " " + targetRot);
        transform.rotation = Quaternion.Euler(newRot);
        
    }


    public void setTargetRot(Vector3 dir) {
        if (Quaternion.Angle( Quaternion.Euler(targetRot),transform.rotation ) > 0.1) {
            return;
        }

        targetRot = transform.eulerAngles + 90 * dir;
        targetRot.x = getAngleBetween0And360(targetRot.x);
        targetRot.y = getAngleBetween0And360(targetRot.y);
        targetRot.z = getAngleBetween0And360(targetRot.z);
    }

    float getAngleBetween0And360(float angle) {
        if (angle < 0) {
            angle += 360;
        }
        angle = angle % 360;
        return angle;
    }
}
