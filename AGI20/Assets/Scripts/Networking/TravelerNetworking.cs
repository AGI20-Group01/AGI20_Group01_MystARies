using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerNetworking : MonoBehaviour
{
    Animator anim;


    private Vector3 prePos = new Vector3(0,0,0);
    private Vector3 preRot = new Vector3(0,0,0);
    private Vector3 targetPos = new Vector3(0,0,0);
    private Vector3 targetRot = new Vector3(0,0,0);

    public int aimatTime = 10;
    private int time = 0; 

    public bool controling = false;
    public float lerpSpeed = 15;


    [SerializeField]
    private NetworkClient networkClient;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        targetPos = transform.position;
        targetRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!controling) {

            Vector3 pos = transform.position;
            Vector3 nextPos = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
            transform.position = nextPos;   //Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
            if (Mathf.Abs(pos.x - nextPos.x) > 0.00001f || Mathf.Abs(pos.z - nextPos.z) > 0.00001f) {
                anim.SetBool("Running", true);
            } else {
                anim.SetBool("Running", false);
            }

            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(rot, targetRot, lerpSpeed * Time.deltaTime));

            /*Vector3 newPos = transform.position;

            if (Mathf.Round(prePos.z * 1000) / 1000 != Mathf.Round(newPos.z * 1000) / 1000 || Mathf.Round(prePos.x * 1000) / 1000 != Mathf.Round(newPos.x * 1000) / 1000 || time >= 0) {
                anim.SetBool("Running", true);
                if (Mathf.Round(prePos.z * 1000) / 1000 != Mathf.Round(newPos.z * 1000) / 1000 || Mathf.Round(prePos.x * 1000) / 1000 != Mathf.Round(newPos.x * 1000) / 1000) {
                    time = aimatTime;
                } else {
                    time--;
                }
            } else {
                anim.SetBool("Running", false);
            }
            prePos = transform.position;*/
        } 
        
        
        else {
            Vector3 newRot = transform.rotation.eulerAngles;
            newRot.x = Mathf.Round(newRot.x * 1000);
            newRot.y = Mathf.Round(newRot.y * 1000);
            newRot.z = Mathf.Round(newRot.z * 1000);

            Vector3 newPos = transform.position;
            newPos.x = Mathf.Round(newPos.x * 1000);
            newPos.y = Mathf.Round(newPos.y * 1000);
            newPos.z = Mathf.Round(newPos.z * 1000);

            if (networkClient != null) {
                if (newPos != prePos) {
                    networkClient.sendMoveTraveler(newPos);
                }
                if (preRot != newRot) {
                    networkClient.sendRotate(newRot);
                }
            }
            prePos = newPos;
            preRot = newRot;
        }
        
    }


    public void setTargetPos(Vector3 pos) {
        targetPos = pos;
    }

    public void setTargetRot(Vector3 rot) {
        targetRot = rot;
    }
}
