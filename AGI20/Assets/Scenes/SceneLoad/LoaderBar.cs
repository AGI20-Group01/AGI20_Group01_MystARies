using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderBar : MonoBehaviour
{
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    } 

    void Update()
    {
        image.fillAmount = 1 - SceneHandler.GetLoadingProgress();
    }
}
