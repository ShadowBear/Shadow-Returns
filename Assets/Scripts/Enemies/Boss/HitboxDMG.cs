using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDMG : MonoBehaviour {

    public int meeleDMG = 35;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<HealthScript>().TakeDamage(meeleDMG);
        }
    }
}
