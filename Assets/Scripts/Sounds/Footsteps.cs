using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    public bool walking = false;
    [SerializeField]
    private float stepRange = 0.25f;
    [SerializeField]
    private float pitch = 0.1f;
    [SerializeField]
    private float volumeRange = 0.1f;
    private AudioSource stepSource;
    private float startVolume;

    public AudioClip step1;
    public AudioClip step2;
    private bool swap = false;

    private float timer = 0;

	// Use this for initialization
	void Start () {
        stepSource = GetComponent<AudioSource>();
        startVolume = stepSource.volume;
	}
	
	// Update is called once per frame
	void Update () {
        if (walking && timer <= 0)
        {
            stepSource.clip = swap ? step1 : step2;
            swap = swap ? false : true;
            stepSource.pitch = Random.Range(1-pitch, 1+pitch);
            stepSource.volume = startVolume + Random.Range(-volumeRange, volumeRange);
            stepSource.Play();
            timer = stepRange;
        }
        else if (timer > 0) timer -= Time.deltaTime;
	}
}
