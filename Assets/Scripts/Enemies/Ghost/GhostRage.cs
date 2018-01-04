using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRage : MonoBehaviour {

    public GameObject spawnParent;
    public GameObject ragingGhost;
    //Adjust the Spawnrate Standart 1
    public float spawnCounterOffset = 1f;

    private Transform[] spawnPoints;
    private GameObject ghostKiller;
    private bool canSpawn = true;

    // Use this for initialization
	void Start () {
        spawnPoints = spawnParent.GetComponentsInChildren<Transform>();
        ghostKiller = GameObject.FindGameObjectWithTag("GhostKiller");
    }
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {
            StartCoroutine(SpawnGhosts());
        }
	}

    IEnumerator SpawnGhosts()
    {
        canSpawn = false;
        int spawnNumber = (int)Random.Range(0, (spawnPoints.Length - 1));
        Transform sp = spawnPoints[spawnNumber];
        sp.LookAt(ghostKiller.transform);
        Instantiate(ragingGhost, sp.position, sp.rotation);
        float spawnCounter = Random.Range(0.1f, 0.2f) * spawnCounterOffset;
        yield return new WaitForSeconds(spawnCounter);
        canSpawn = true;
        yield return null;

    }
}
