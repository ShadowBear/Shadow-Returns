using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena01Controller : MonoBehaviour {


    public GameObject[] chainBlocks;
    public GameObject ghostDoorKepper;
    public GameObject enemyGhost;
    public GameObject enemyZombie;
    public GameObject bigSlime;
    public float spawnTime = 0.5f;

    public Transform spawenPointTransform;

    public int spawnCounter = 5;
    private Vector3 spawnPoint = Vector3.zero;
    private bool waveStarted = false;
    private bool spawned = false;
    public int waveCounter = 3;

    // Use this for initialization
    void Start () {
        SetChainBlocks(false);
        ghostDoorKepper.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (waveStarted)
        {
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                waveStarted = false;
                waveCounter--;
                if (waveCounter > 0) StartCoroutine(SpawnEnemies(spawnTime));
                else ArenaComplete();
                if(waveCounter == 1)
                {
                    Instantiate(bigSlime, transform.position, transform.rotation);
                }
               //Next Wave Starts
               //print("Nächste Wave");
            }
        }
	}

    void SetChainBlocks(bool active)
    {
        foreach(GameObject chain in chainBlocks)
        {
            chain.SetActive(active);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !spawned)
        {
            SetChainBlocks(true);
            spawned = true;
            //Spawnen der Gegner
            StartCoroutine(SpawnEnemies(spawnTime));
        }
    }

    IEnumerator SpawnEnemies(float time)
    {
        for (int i = 0; i < spawnCounter; i++)
        {
            spawnPoint = new Vector3(spawenPointTransform.position.x + Random.Range(-3, 3), spawenPointTransform.position.y, spawenPointTransform.position.z + Random.Range(-3, 3));
            GameObject zombie = Instantiate(enemyZombie, spawnPoint, transform.rotation);
            zombie.GetComponent<HealthScript>().SetHealth(30);
        }
            for (int i = 0; i < spawnCounter; i++)
        {
            spawnPoint = new Vector3(spawenPointTransform.position.x + Random.Range(-5, 5), spawenPointTransform.position.y, spawenPointTransform.position.z + Random.Range(-5, 5));
            GameObject ghost = Instantiate(enemyGhost, spawnPoint, transform.rotation);
            ghost.GetComponent<HealthScript>().SetHealth(15);
            yield return new WaitForSeconds(time);
        }
        waveStarted = true;
    }

    void ArenaComplete()
    {
        SetChainBlocks(false);
        ghostDoorKepper.SetActive(true);
    }

}
