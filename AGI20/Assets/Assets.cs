using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Assets : MonoBehaviour
{
    private static Assets _i;

    public static Assets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<Assets>("Assets"));
            return _i;
        }
    }

    public Transform PingPosition;
    public Transform PingLocator;
}
