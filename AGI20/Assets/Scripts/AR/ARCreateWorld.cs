using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCreateWorld : MonoBehaviour
{
    public GameObject worldObject;
    private ARCursorScript cursorIndicator;

    private void Start()
    {
        cursorIndicator = FindObjectOfType<ARCursorScript>();
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(worldObject, cursorIndicator.transform.position, cursorIndicator.transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
