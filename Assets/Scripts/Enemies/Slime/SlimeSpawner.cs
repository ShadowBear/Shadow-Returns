using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSpawner : MonoBehaviour {


    public GameObject endlessSlime;
    public int maxSlimes = 4;
    public float minSpeed = .75f;
    public float maxSpeed = 1.75f;

    public Transform[] spawnPoints;

    private bool canSpawn = true;
    public float spawnTimer = 0.75f;
    private float count;
    // Use this for initialization
	void Start () {
        count = spawnTimer;

    }
	
	// Update is called once per frame
	void Update () {

        /******** Spawn all missing slimes at the same time *********/
        ////  if (activated)
        // // {
        //      if (GameObject.FindGameObjectsWithTag("Slime").Length < maxSlimes)
        //      {
        //          int slimCount = GameObject.FindGameObjectsWithTag("Slime").Length;
        //          for (int i = slimCount; i < maxSlimes; i++)
        //          {
        //              int pos = (int)Random.Range(0, spawnPoints.Length - 1);
        //              GameObject slime = Instantiate(endlessSlime, spawnPoints[pos].position, endlessSlime.transform.rotation);
        //              slime.GetComponent<NavMeshAgent>().speed = Random.Range(minSpeed,maxSpeed);
        //          }
        //      }
        //  //}

        /******** Spawn the missing slimes each after a delay of X seconds  *********/
        if (GameObject.FindGameObjectsWithTag("Slime").Length < maxSlimes && canSpawn)
        {            
            int pos = Random.Range(0, spawnPoints.Length);
            GameObject slime = Instantiate(endlessSlime, spawnPoints[pos].position, endlessSlime.transform.rotation);
            slime.GetComponent<NavMeshAgent>().speed = Random.Range(minSpeed, maxSpeed);
            count = spawnTimer;
            canSpawn = false;
        }
        if (!canSpawn) count -= Time.deltaTime;
        if (count <= 0) canSpawn = true;

    }
}
