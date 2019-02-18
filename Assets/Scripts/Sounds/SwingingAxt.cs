using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxt : MonoBehaviour {

    // Use this for initialization

    public AudioSource audioSource;
	void Start () {
        InvokeRepeating("PlaySound", 0.5f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlaySound()
    {
        StartCoroutine(Sound());
    }

    IEnumerator Sound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1.25f);
        audioSource.Stop();
        yield return null;

    }

}
