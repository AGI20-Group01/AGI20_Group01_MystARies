using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class OnScreenIndicator : MonoBehaviour
{
    public GameObject target;
    private Image image;
    private RectTransform rectTransform;
    Vector2 indicatorPos;

    public void setUp(Vector3 pos)
    {
        target = GameObject.Find("Collectable");
        rectTransform = transform.GetComponent<RectTransform>();
        image = transform.GetComponent<Image>();
    }

    void Update ()
    {
        Vector2 targetScreenCoordinates = Camera.main.WorldToScreenPoint(target.transform.position);
        bool isOffScreen =
            targetScreenCoordinates.x > Screen.width ||
            targetScreenCoordinates.x < 0 ||
            targetScreenCoordinates.y > Screen.height ||
            targetScreenCoordinates.y < 0;

        image.enabled = isOffScreen;

        if (isOffScreen)
        {
            // Update UI position
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (target.transform.position - fromPosition).normalized;

            float uiRadius = 270f;
            rectTransform.anchoredPosition = dir * uiRadius;
        }
        else
        {
            // target is on screen
            // UI element hidden
        }





        /*   Vector2 screenpos = Camera.main.WorldToScreenPoint(target.transform.position);

           (float x, float y) centerPos = (screenpos.x - Screen.width / 2, screenpos.y - Screen.height / 2);

           bool isOnScreen = checkTargetonScreen(centerPos);
           image.enabled = isOnScreen;

           if(!isOnScreen)
           {
               float rot = Mathf.Atan2(centerPos.y, centerPos.x) * Mathf.Rad2Deg;

               float slope = centerPos.y / centerPos.x;

               // Adding a bit of padding to make it not hug the edge
               int padding = 30;
               (float width, float height) padSize = (Screen.width - padding, Screen.height - padding);

               if (centerPos.y < 0)
               {
                   indicatorPos = new Vector2((-padSize.height / 2) / slope, (-padSize.height / 2));
               }
               else
               {
                   indicatorPos = new Vector2((padSize.height / 2) / slope, (padSize.height / 2));
               }

               if (indicatorPos.x < -padSize.height / 2)
               {
                   indicatorPos = new Vector2(-padSize.width / 2, slope * (-padSize.height / 2));
               }
               else if (indicatorPos.y > padSize.width / 2)
               {
                   indicatorPos = new Vector2(padSize.width / 2, slope * (padSize.height / 2));
               }

               indicatorPos = new Vector2(indicatorPos.x + screenpos.x + Screen.width / 2, indicatorPos.y + screenpos.y + Screen.height / 2);

               this.gameObject.transform.position = indicatorPos;
           }
       }

       bool checkTargetonScreen((float x, float y) target)
       {
           if(target.y > 0 && target.y < Screen.height && target.x < Screen.width && target.x > 0)
           {
               return true;
           }
           else
           {
               return false;
           }*/
       }
    }
