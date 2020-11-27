using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MobilUIAnimator : MonoBehaviour
{

    private RectTransform mobil;

    public float RotateSpeed = 5f;
    public float Radius = 0.1f;

    private float angle;
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        mobil = GetComponent<RectTransform>();
        center = mobil.position;
    }

    // Update is called once per frame
    void Update()
    {

        angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * Radius;
        mobil.localPosition = offset; 
    
    }
}
