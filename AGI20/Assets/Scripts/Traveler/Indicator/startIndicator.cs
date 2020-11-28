using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startIndicator : MonoBehaviour
{
    public AudioSource collectSound;

    void Awake()
    {
        IndicatorWindow.AddIndicator(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        ScorePointSystem.score += 1;
        Destroy(gameObject);
    }
}
