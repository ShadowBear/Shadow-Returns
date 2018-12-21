using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour {

    [SerializeField]
    private float pitch = 0.1f;
    [SerializeField]
    private float volumeRange = 0.1f;
    private AudioSource stepSource;
    private float startVolume;

    // Use this for initialization
    void Start()
    {
        stepSource = GetComponent<AudioSource>();
        startVolume = stepSource.volume;
    }

    // Update is called once per frame
    private void Swing()
    {        
        stepSource.pitch = Random.Range(1 - pitch, 1 + pitch);
        stepSource.volume = startVolume + Random.Range(-volumeRange, volumeRange);
        stepSource.Play();
    }
}
