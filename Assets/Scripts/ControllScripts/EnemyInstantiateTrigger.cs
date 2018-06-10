using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiateTrigger : MonoBehaviour {

    // Use this for initialization
    public GameObject[] enemyTyp;
    public GameObject spawnPointParent;
    public int enemyStartHealth = 50;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Triggered()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1f);
        foreach (Transform child in spawnPointParent.GetComponentsInChildren<Transform>())
        {
            if (child != spawnPointParent.transform)
            {
                foreach (GameObject enemy in enemyTyp)
                {
                    GameObject enemyInstance = Instantiate(enemy, child.position, Quaternion.identity);
                    HealthScript enemyHealth = enemyInstance.GetComponent<HealthScript>();
                    if (enemyHealth != null) enemyHealth.SetHealth(enemyStartHealth);
                    //yield return new WaitForSeconds(0.25f);
                }
            }
        }
    }

    public void TriggeredWithPos()
    {
        if(enemyTyp.Length == (spawnPointParent.GetComponentsInChildren<Transform>().Length - 1))
        {
            for (int i = 0; i < enemyTyp.Length; i++)
            {
                Instantiate(enemyTyp[i], spawnPointParent.transform.GetChild(i).transform.position, Quaternion.identity);                    
            }
        }
    }
}
