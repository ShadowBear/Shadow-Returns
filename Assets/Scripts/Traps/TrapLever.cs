using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLever : MonoBehaviour {

    public bool isActivated = false;
    public bool repeatTrap = false;
    [SerializeField]
    private float repeatTime = 3f;
    //private bool moving = false;
    Animator anim;

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("isActivated", isActivated);
        if (repeatTrap) InvokeRepeating("TriggerTrap", 1.5f, repeatTime);
	}
	
	public void TriggerTrap()
    {
        isActivated = isActivated ? false : true;
        anim.SetBool("isActivated", isActivated);
            
    }

    public void DeactivateTrap()
    {
        CancelInvoke();
        isActivated = false;
        anim.SetBool("isActivated", isActivated);
    }


}
