using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PingSystem
{
    public static void AddPing(Vector3 position)
    {
        Object.Instantiate(Assets.i.PingPosition, position, Quaternion.identity);
        Debug.Log("Ping added");
        //PingWindow.AddPing(position);
    }
}
