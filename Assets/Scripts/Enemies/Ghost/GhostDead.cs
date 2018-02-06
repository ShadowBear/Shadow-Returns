using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDead : MonoBehaviour {

    public GameObject cagedGhost;
    private Animator anim;

    void Start()
    {
        //print("RagedGhost is das");
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("GhostKiller") || col.CompareTag("Player") || col.CompareTag("Shot"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (anim != null)
        {
            anim.SetBool("die", true);
            anim.SetTrigger("damaged");
        }
        yield return new WaitForSeconds(1f);
        if (cagedGhost != null)
        {
            cagedGhost.SetActive(true);
        }
        Destroy(gameObject);
        yield return null;
    }
    //void OnTriggerStay(Collider col)
    //{
    //    if (col.CompareTag("GhostKiller"))
    //    {
    //        if (cagedGhost != null)
    //        {
    //            cagedGhost.SetActive(true);
    //        }
    //        Destroy(gameObject);
    //    }
    //}
}
