using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerNetworking : MonoBehaviour
{
    Animator anim;


    private Vector3 prePos = new Vector3(0,0,0);
    private Vector3 preRot = new Vector3(0,0,0);

    public int aimatTime = 10;
    private int time = 0; 

    public bool controling = false;


    [SerializeField]
    private NetworkClient networkClient;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!controling) {
            Vector3 newPos = transform.position;
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
            prePos = transform.position;
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
}
