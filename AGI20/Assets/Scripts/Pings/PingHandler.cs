using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingHandler : MonoBehaviour
{
    Vector3 cameraPos;

    public void sendPing()
    {
        Camera camera = Camera.main;
        cameraPos = camera.ScreenToWorldPoint(new Vector3(Screen.width / 4, Screen.height / 4, camera.nearClipPlane));
        PingSystem.AddPing(cameraPos);

        //Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        //RaycastHit hit;
        //Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        /*if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitpos = hit.transform.position;

            Vector3 position = hitpos + hit.normal;

            PingSystem.AddPing(new Vector3(0.5f, 0.5f, 0f));
        }*/
    }
}
