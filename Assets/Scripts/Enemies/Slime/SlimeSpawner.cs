using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSpawner : MonoBehaviour {


    public GameObject endlessSlime;
    public int maxSlimes = 4;

    public Transform[] spawnPoints;
    //public bool activated = false;
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
      //  if (activated)
       // {
            if (GameObject.FindGameObjectsWithTag("Slime").Length < 4)
            {
                int slimCount = GameObject.FindGameObjectsWithTag("Slime").Length;
                for (int i = slimCount; i < maxSlimes; i++)
                {
                    int pos = (int)Random.Range(0, spawnPoints.Length - 1);
                    GameObject slime = Instantiate(endlessSlime, spawnPoints[pos].position, spawnPoints[pos].rotation);
                slime.GetComponent<NavMeshAgent>().speed = 1;
                }
            }
        //}
	}
}
