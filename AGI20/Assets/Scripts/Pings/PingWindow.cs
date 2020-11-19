using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingWindow : MonoBehaviour
{
    private static PingWindow instance;

    private void Awake()
    {
        instance = this;
    }

    public static void AddPing(Vector3 position)
    {
        Transform pingLocatorTransform = Instantiate(Assets.i.PingLocator, instance.transform);
        pingLocatorTransform.GetComponent<PingUIHandler>().Setup(position);
        Debug.Log("Ping added");
    }
}
