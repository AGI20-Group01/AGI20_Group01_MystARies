using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void startSpirit()
    {
        SceneManager.LoadScene("SpiritScene");
        //SceneHandler.Load(SceneHandler.Scene.SpiritScene);
    }

    public void startTraveller()
    {
        SceneManager.LoadScene("TravellerAR");
        //SceneHandler.Load(SceneHandler.Scene.TravellerAR);
    }

    public void startMenu()
    {
        SceneManager.LoadScene("test");
        //SceneHandler.Load(SceneHandler.Scene.TravellerAR);
    }
}
