using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDMG : MonoBehaviour {

    public int meeleDMG = 35;
    //Chance to Make Crit Values between 0-1f : 0 = 0%  and 1 = 100%
    public float criticalChance = 0.15f;
    private int critMultiplier = 0;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(meeleDMG);
        }
        if (col.CompareTag("Enemy") || col.CompareTag("Destroyable"))
        {
            //Apply Damage
            col.GetComponent<HealthScript>().TakeDamage(CalculateDmg());
        }
        else if (col.CompareTag("Boss"))
        {
            col.GetComponent<LibaryGhostBossHealth>().TakeDamage(CalculateDmg());
        }
        else if (col.CompareTag("Slime"))
        {
            //col.GetComponent<SlimeHealth>().TakeDamage(CalculateDmg());
            col.GetComponent<SlimeHealthChild>().TakeDamage(CalculateDmg());            
        }
    }

    int CalculateDmg()
    {
        int dmgVarianz = Random.Range(1, 11) - 6;
        if (Random.Range(0f, 1f) < criticalChance) critMultiplier = 2;
        else critMultiplier = 1;
        return (meeleDMG + dmgVarianz) + ((critMultiplier * meeleDMG) - meeleDMG);
    }
}
