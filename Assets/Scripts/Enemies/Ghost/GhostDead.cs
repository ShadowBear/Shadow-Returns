using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDead : MonoBehaviour {

    public GameObject cagedGhost;

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
}
