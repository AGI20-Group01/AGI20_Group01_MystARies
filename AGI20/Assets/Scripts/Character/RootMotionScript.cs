using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
    
public class RootMotionScript : MonoBehaviour {

    private float rotationSpeed;
    private float y;

    private bool backwards;

    void Start()
    {
        y = 0.0f;
        rotationSpeed = 600.0f;
    }
            
    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>(); 
                            
        if (animator)
        {
            backwards = animator.GetBool("Backwards");

            Debug.Log("vertical:"+animator.GetFloat("vertical"));
            Vector3 newPosition = transform.position;
            newPosition.z += animator.GetFloat("vertical") * Time.deltaTime; 
            newPosition.x += animator.GetFloat("horizontal") * Time.deltaTime;
            transform.position = newPosition;

        
            if (animator.GetFloat("vertical")<-0.1f)
            {
                if (backwards == false)
                {
                    if(animator.GetFloat("horizontal")<-0.1f)
                    {
                        y =  y - Time.deltaTime * rotationSpeed < 0 ? 360 - Time.deltaTime * rotationSpeed : y - Time.deltaTime * rotationSpeed;

                        if (y < 180.0f)
                        {
                            y = 180.0f;
                            backwards = true;
                            animator.SetBool("Backwards", true);
                        }
                    }
                    else
                    {
                        y += Time.deltaTime * rotationSpeed;

                        if (y > 180.0f)
                        {
                            y = 180.0f;
                            backwards = true;
                            animator.SetBool("Backwards", true);
                        }
                    }
                }
            }
            else if (animator.GetFloat("vertical")>0.1f)
            {
                if (backwards == true)
                    {
                    if(animator.GetFloat("horizontal")<-0.1f)
                    {
                        y += Time.deltaTime * rotationSpeed;
                        if (y > 360.0f)
                        {
                            y = 0.0f;
                            backwards = false;
                            animator.SetBool("Backwards", false);
                        }
                    }
                    else 
                    {
                       y -= Time.deltaTime * rotationSpeed;
                        if (y < 0.0f)
                        {
                            y = 0.0f;
                            backwards = false;
                            animator.SetBool("Backwards", false);
                        } 
                    }
                }
                
            }
            transform.localRotation = Quaternion.Euler(0, y, 0);

        }
    }

    
}


