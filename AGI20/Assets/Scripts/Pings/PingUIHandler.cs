using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PingUIHandler : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 pingPostion;
    private TextMeshPro distanceText;
    private Image image;
    public GameObject arCameraObject;

    public void Setup(Vector3 pingPos)
    {
        this.pingPostion = pingPos;
        rectTransform = transform.GetComponent<RectTransform>();
        image = transform.GetComponent<Image>();
        distanceText = transform.Find("distanceText").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        Vector2 pingScreenCoordinates = Camera.main.WorldToScreenPoint(pingPostion);
        bool isOffScreen = pingScreenCoordinates.x > Screen.width || pingScreenCoordinates.x < 0 || pingScreenCoordinates.y > Screen.height || pingScreenCoordinates.y < 0;

        image.enabled = isOffScreen;
        distanceText.enabled = isOffScreen;

        // Update UI
        Vector3 fromPosition = Camera.main.transform.position;
        Vector3 dir = (pingPostion - fromPosition).normalized;

        float uiRadius = 270f;
        rectTransform.anchoredPosition = dir * uiRadius;

        // Update Distance-text
        Vector3 pingPos = transform.parent.position;
        Vector3 cameraPos = arCameraObject.transform.position;
        int distance = Mathf.RoundToInt(Vector3.Distance(pingPos, cameraPos) / 3f);
        distanceText.text = distance + " m";
    }
}
