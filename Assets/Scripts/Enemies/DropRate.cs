using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRate : MonoBehaviour {


    [SerializeField]
    private GameObject[] drops;
    //Amout of drops for later use
    //public int dropCounts;
    
    [SerializeField]
    private GameObject ep;   
    [SerializeField]
    private int epValue = 11;
    private float explosionRadius = 5;
    private int explosionForce = 250;
    private Vector3 offset;

    //DropRate in Percent 0-1
    [Range(0,1)]
    public float dropRate;

    void Start()
    {
        offset = new Vector3(0, 1, 0);
    }


    public void DropItem()
    {
        if(Random.Range(0, 1) <= dropRate)
        {
            if(drops.Length > 0)
            {
                int item = Random.Range(0, drops.Length);
                Instantiate(drops[item], transform.position, transform.rotation);
            }            
        }
        GameObject epObject = Instantiate(ep, transform.position + offset, transform.rotation);
        epObject.GetComponentInChildren<Experience>().experience = epValue;
        Vector3 randomExplosionVector = new Vector3(Random.Range(-1f, 1f), 0.9f, Random.Range(-1f, 1f));
        epObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position + randomExplosionVector, explosionRadius);
    }

}
