using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void startSpirit()
    {
        SceneHandler.Load(SceneHandler.Scene.SpiritScene);
    }

    public void startTraveller()
    {
        SceneHandler.Load(SceneHandler.Scene.TravellerAR);
    }
}
