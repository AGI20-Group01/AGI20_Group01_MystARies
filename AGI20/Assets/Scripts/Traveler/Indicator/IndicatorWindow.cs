using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorWindow : MonoBehaviour
{
    private static IndicatorWindow instance;

    private void Awake()
    {
        instance = this;
    }

    public static void AddIndicator(Vector3 pos)
    {
        Transform IndicatorUI = Instantiate(GameAssets.i.IndicatorUI, instance.transform);
        IndicatorUI.GetComponent<OnScreenIndicator>().setUp(pos);
    }
}
