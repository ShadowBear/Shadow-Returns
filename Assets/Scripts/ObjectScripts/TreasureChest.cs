using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    private Animator anim;
    private bool open = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (anim) anim.SetTrigger("Shake");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
            {
                open = open ? false : true;
                if (anim) anim.SetBool("Open", open);
                //print("E gedrückt");
            }
        }
    }
}
