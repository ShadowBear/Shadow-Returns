using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDead : MonoBehaviour {

    public GameObject cagedGhost;

    void Start()
    {
        print("RagedGhost is das");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("GhostKiller"))
        {
            if(cagedGhost != null)
            {
                cagedGhost.SetActive(true);
            }
            Destroy(gameObject);
        }
       
    }
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("GhostKiller"))
        {
            if (cagedGhost != null)
            {
                cagedGhost.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
