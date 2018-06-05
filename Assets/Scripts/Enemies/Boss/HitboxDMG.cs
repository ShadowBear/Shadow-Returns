using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDMG : MonoBehaviour {

    public int meeleDMG = 35;
    //Chance to Make Crit Values between 0-1f : 0 = 0%  and 1 = 100%
    public float criticalChance = 0.15f;
    private int critMultiplier = 0;
    bool meleeAttack = true;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(meeleDMG, meleeAttack);
        }
        if (col.CompareTag("Enemy") || col.CompareTag("Destroyable"))
        {
            //Apply Damage
            if (col.GetComponent<EnemyHealth>()) col.GetComponent<EnemyHealth>().TakeDamage(CalculateDmg(), meleeAttack);
            else if (col.GetComponent<HealthScript>()) col.GetComponent<HealthScript>().TakeDamage(CalculateDmg(), meleeAttack);
        }
        else if (col.CompareTag("Boss"))
        {
            col.GetComponent<LibaryGhostBossHealth>().TakeDamage(CalculateDmg());
        }
        else if (col.CompareTag("Slime"))
        {
            //col.GetComponent<SlimeHealth>().TakeDamage(CalculateDmg());
            col.GetComponent<SlimeHealthChild>().TakeDamage(CalculateDmg(), meleeAttack);            
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
