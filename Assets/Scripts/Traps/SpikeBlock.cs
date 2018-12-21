using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour {

    // Use this for initialization
    private Animator anim;
    public bool activated;

	void Start () {
        anim = transform.parent.GetComponentInChildren<Animator>();        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activated)
            {
                anim.SetTrigger("isActivated");
            }
        }
    }

}
