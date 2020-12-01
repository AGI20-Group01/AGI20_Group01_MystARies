using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateMe : MonoBehaviour
{
    OnScreenIndicator UIIndicator;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject Indicator = GameObject.FindWithTag("Indicator");
        if (Indicator != null)
        {
            UIIndicator = Indicator.GetComponent<OnScreenIndicator>();
        }
    }
}
