using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrians : MonoBehaviour {



    Animator anim;
    private float delayTime;
    public float maxDelayTime = 35f;
    public float minDelayTime = 20f;


    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        delayTime = Random.Range(minDelayTime, maxDelayTime);
    }
	
	// Update is called once per frame
	void Update () {
        delayTime -= Time.deltaTime;
        if(delayTime <= 0)
        {
            AnimationControl();
            delayTime = Random.Range(minDelayTime, maxDelayTime);
        }
	}

    /*  Animation_int - selects an idle animation to play
        0 = normal idle
        1 = Crossed Arms
        2 = HandsOnHips
        3 = Check Watch
        4 = Sexy Dance
        5 = Smoking
        6 = Salute
        7 = Wipe Mount
        8 = Leaning against wall
        9 = Sitting on Ground */

    void AnimationControl()
    {
        int animation = Random.Range(0, 4);
        if (animation == 3) animation = 9;
        anim.SetInteger("Animation_int", animation);
    }

}
