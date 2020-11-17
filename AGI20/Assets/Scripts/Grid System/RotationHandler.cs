using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    private Vector3 targetRot; 
    private Vector3 startRot;

    public float rotSpeed;

    [SerializeField]
    private List<Transform> galaxyCubes = new List<Transform>();

    private float t;

    // Start is called before the first frame update
    void Start()
    {
        GalaxyCube[] gCubes = GameObject.FindObjectsOfType<GalaxyCube>();
        for (int i = 0; i < gCubes.Length; i++) {
            galaxyCubes.Add(gCubes[i].transform);
        }
        

        targetRot = transform.eulerAngles;
        startRot = transform.eulerAngles;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        handleInputs();
        updateRotation();
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
        t += Time.deltaTime * rotSpeed;

        Vector3 newRot = Quaternion.Lerp(Quaternion.Euler(startRot), Quaternion.Euler(targetRot), t).eulerAngles;
        if (Quaternion.Angle( Quaternion.Euler(targetRot),transform.rotation ) < 0.1) {
            t = 0;
            startRot = targetRot;
            newRot = targetRot;
        }
        //Debug.Log(transform.eulerAngles + " " + targetRot);
        transform.rotation = Quaternion.Euler(newRot);

        for (int i = 0; i < galaxyCubes.Count; i++) {
            galaxyCubes[i].rotation = Quaternion.identity;
        }
        
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