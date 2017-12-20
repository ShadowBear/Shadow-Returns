using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombSpawn : MonoBehaviour {

    public GameObject cagedGhost;
    public GameObject instantiateEnemy;
    public Transform spawnPosition;
    private bool spawned = false;

    public bool onEnter = false;

    void OnTriggerExit(Collider col)
    {
        if (!onEnter)
        {
            if (col.CompareTag("Player"))
            {
                if (!spawned)
                {
                    GameObject ghost = Instantiate(instantiateEnemy, spawnPosition.transform.position, spawnPosition.rotation);
                    ghost.GetComponent<GhostDead>().cagedGhost = cagedGhost;
                    spawned = true;
                }                
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (onEnter)
        {
            if (col.CompareTag("Player"))
            {
                if (!spawned)
                {
                    GameObject ghost = Instantiate(instantiateEnemy, spawnPosition.transform.position, spawnPosition.rotation);
                    ghost.GetComponent<GhostDead>().cagedGhost = cagedGhost;
                    spawned = true;
                }
            }
        }
    }
}
