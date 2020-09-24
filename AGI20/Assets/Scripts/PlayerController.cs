using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator anim;
    
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("horizontal",h);
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("vertical",v);

        if (h!=0||v!=0 )
        {
            anim.SetBool("Running", true);
        }
        else 
        {
            anim.SetBool("Running", false);
        }
        
    }
}


