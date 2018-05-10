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
    public int epValue;

    //DropRate in Percent 0-1
    [Range(0,1)]
    public float dropRate;


    public void DropItem()
    {
        if(Random.Range(0, 1) <= dropRate)
        {
            int item = (int)Random.Range(0, drops.Length);
            Instantiate(drops[item], transform.position, transform.rotation);
        }
        GameObject epObject = Instantiate(ep, transform.position, transform.rotation);
        epObject.GetComponent<Experience>().experience = epValue;
    }
}
