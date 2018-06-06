using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxt : MonoBehaviour {

    // Use this for initialization

    public AudioClip soundclip;
	void Start () {
        InvokeRepeating("PlaySound",0.5f,1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlaySound()
    {
        AudioSource.PlayClipAtPoint(soundclip, transform.position, 1.5f);
    }
}
