using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void startSpirit()
    {
        SceneManager.LoadScene("EmilSpiritScene");
        //SceneHandler.Load(SceneHandler.Scene.SpiritScene);
    }

    public void startTraveller()
    {
        SceneManager.LoadScene("EmilTravellerAR");
        //SceneHandler.Load(SceneHandler.Scene.TravellerAR);
    }

    public void startMenu()
    {
        SceneManager.LoadScene("test");
        //SceneHandler.Load(SceneHandler.Scene.test);
    }
}
