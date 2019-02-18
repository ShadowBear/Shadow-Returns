using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour {


    public static Soundmanager soundmanager;
    public AudioClip startClip;
    private AudioSource audioSource;

    //Only when the trigger is True next audio can be played
    public bool resetTrigger;
    private float resetTimerHysteres = 4;
    private float timer = 0;

    // Use this for initialization
	void Start () {
        if (soundmanager == null) soundmanager = this;
        else if (soundmanager != this) Destroy(gameObject);

        resetTrigger = false;
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.clip = startClip;
        audioSource.Play();
	}

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    public void ChangeAudio(AudioClip nextTrack, float fadeTime)
    {
        if (resetTrigger && timer <= 0)
        {
            resetTrigger = false;
            timer = resetTimerHysteres;
            StartCoroutine(FadeOut(audioSource, nextTrack, fadeTime));
        }
    }

    public void SetStartAudio(AudioClip nextTrack)
    {
        StartCoroutine(FadeOut(audioSource, nextTrack, 0));
    }

    public static IEnumerator FadeOut(AudioSource audio, AudioClip nextTrack, float FadeTime)
    {        
        float startVolume = audio.volume;
        if (FadeTime <= 0) FadeTime = 0.1f;
        if (startVolume <= 0.5 || startVolume > 1) startVolume = 1;

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audio.Stop();
        if(nextTrack != null)
        {
            audio.clip = nextTrack;
            audio.volume = 0;
            audio.Play();
            if(float.IsNaN(startVolume)) startVolume = 1;
            while (audio.volume < startVolume)
            {
                audio.volume += startVolume * Time.deltaTime / FadeTime;                
                yield return null;
            }
            if (audio.volume <= 0 || audio.volume > 1) audio.volume = 1;
        }
    }

}
