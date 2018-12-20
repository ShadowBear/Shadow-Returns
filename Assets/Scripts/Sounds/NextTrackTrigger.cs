using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTrackTrigger : MonoBehaviour {

    public AudioClip track;
    public float fadeTime = 1.5f;


    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Soundmanager.soundmanager.ChangeAudio(track, fadeTime);
        }
    }
}
