using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianAnimationSetter : Pedestrians {

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

    public int animate = 0;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        AnimationControl();
    }
	
	// Update is called once per frame
	new void Update () {
		
	}

    new void AnimationControl()
    {
        anim.SetInteger("Animation_int", animate);
    }
}
