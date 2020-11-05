using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class PlayerTouchController : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        player = FindObjectOfType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitpos = hit.transform.position;
                    player.MoveCharacter(hitpos);
                   // Vector3 position = hitpos + hit.normal;

                }
            }
        }

    }
}
