using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePointSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int score;

    private void Update()
    {
        scoreText.GetComponent<Text>().text = "" + score;
    }
}
