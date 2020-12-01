using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Step(){
        AudioClip clip= GetRandomClip();
        //Debug.Log("Playing audio: ", clip);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip() {
        
        return clips[UnityEngine.Random.Range(0,clips.Length)];

    }
   
}
