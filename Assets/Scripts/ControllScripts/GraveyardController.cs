using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardController : MonoBehaviour {
    [SerializeField]
    private int ghostCounter;
    public GameObject chainLock;
    public GameObject bossGhost;
    public GameObject tower;
    public GameObject[] hidingTombs;
    public SlimeSpawner slimeSpawner;
    public GameObject towerTransformParent;
    private bool allPieces = false;
    
    // Use this for initialization
	void Start () {
        ghostCounter = hidingTombs.Length;
        chainLock.SetActive(false);
        SetHidingTombsShield(true);
        slimeSpawner.enabled = false;

    }

    private void Update()
    {
        if (allPieces)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                chainLock.SetActive(false);
                slimeSpawner.enabled = false;
                bossGhost.GetComponent<GraveyardBoss>().Die();
                Destroy(gameObject);
            }            
        }
    }

    public void GhostKilled()
    {
        ghostCounter--;
        if(ghostCounter <= 0)
        {
            allPieces = true;
        }
    }

    public void SetHidingTombsShield(bool state)
    {
        foreach(GameObject tomb in hidingTombs)
        {
            tomb.GetComponent<HealthScript>().isShielded = state;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chainLock.SetActive(true);
            SetHidingTombsShield(false);
            bossGhost.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            slimeSpawner.enabled = true;
            ActivateTowers();
        }
    }

    private void ActivateTowers()
    {
        foreach(Transform child in towerTransformParent.transform)
        {
            GameObject towerObject = Instantiate(tower, child.transform.position, child.transform.rotation);
            towerObject.transform.position += new Vector3(0,0.865f,0);
            towerObject.GetComponent<HealthScript>().SetHealth(30);
        }
    }

    public int GetGhostPieces()
    {
        return ghostCounter;
    }
}
