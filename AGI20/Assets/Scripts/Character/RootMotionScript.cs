using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
    
public class RootMotionScript : MonoBehaviour {
            
    void OnAnimatorMove()
    {
            Animator animator = GetComponent<Animator>(); 
                              
            if (animator)
            {
                Debug.Log(animator);
     Vector3 newPosition = transform.position;
               newPosition.z += animator.GetFloat("vertical") * Time.deltaTime; 
               newPosition.x += animator.GetFloat("horizontal") * Time.deltaTime;
     transform.position = newPosition;
            }
    }
}
