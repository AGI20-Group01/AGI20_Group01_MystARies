using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    //public AudioSource collectSound;

    private void OnTriggerEnter(Collider other)
    {
        //collectSound.Play();
        ScorePointSystem.score += 1;
        Destroy(gameObject);
    }
}
