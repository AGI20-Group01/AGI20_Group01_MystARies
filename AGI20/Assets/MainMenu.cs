using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Runtime.Hosting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        UnityEngine.Debug.Log("Quit");
        Application.Quit();
    }
}
