using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
   NavMeshAgent agent;

   void Start() 
   {
       agent = GetComponent<NavMeshAgent> ();
   }

   void Update()
   {
       if(Input.GetMouseButtonDown(0))
       {
           RaycastHit hit;
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if(Physics.Raycast(ray, out hit, Mathf.Infinity)) //Raycast(the ray, the hitpoint, the maximum length of our ray)
           {
               agent.SetDestination(hit.point);
           }
       }
   }
}
