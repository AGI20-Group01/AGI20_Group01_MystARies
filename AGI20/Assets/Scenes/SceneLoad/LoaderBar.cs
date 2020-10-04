using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderBar : MonoBehaviour
{
    private Image image;

    void Awake()
    {
        image.transform.GetComponent<Image>();
    } 

    void Update()
    {
        image.fillAmount = SceneHandler.GetLoadingProgress();
    }
}
