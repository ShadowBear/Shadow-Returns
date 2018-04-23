using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaver : MonoBehaviour {

    private bool isActivated = false;
    private Animator anim;
    public GameObject toOpenObject;


    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("isActivated", isActivated);
        toOpenObject.SetActive(!isActivated);
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isActivated = isActivated ? false : true;
                anim.SetBool("isActivated", isActivated);
                toOpenObject.SetActive(!isActivated);
            }
        }
    }
}
