using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Animator anim;
    
    
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay(){
        isGrounded = true;
    }


    // Update is called once per frame
    private void Update()
    {
        //Jump
         if(Input.GetKeyDown(KeyCode.Space) && isGrounded){

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        //move in x,z
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


