using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartConfig : MonoBehaviour {
    public AudioClip startSoundTrack;

    // Use this for initialization
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        if (startSoundTrack) GameObject.FindGameObjectWithTag("Soundbox").
            GetComponentInParent<Soundmanager>().startClip = startSoundTrack;
    }

    private void Start()
    {
        if (startSoundTrack)
        {
            Soundmanager.soundmanager.SetStartAudio(startSoundTrack);
            print("ChangeTrack");
        }
        Destroy(gameObject);
    }
}
