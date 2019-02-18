using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDMG : MonoBehaviour {

    [SerializeField]
    private int meleeDMG = 10;
    //Chance to Make Crit Values between 0-1f : 0 = 0%  and 1 = 100%
    public float criticalChance = 0.15f;
    private float critMultiplier = 1;
    bool meleeAttack = true;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(meleeDMG, meleeAttack);
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
            col.GetComponent<SlimeHealthChild>().TakeDamage(CalculateDmg(), meleeAttack);            
        }
        else if (col.CompareTag("Explosion"))
        {
            col.GetComponent<ExplosionItems>().TakeDamage(CalculateDmg());
        }
    }

    int CalculateDmg()
    {
        int weaponVarianz = GetComponent<Weapon>().damageVarianz;
        meleeDMG = GetComponent<Weapon>().damageAmount;
        int dmgVarianz = Random.Range(-weaponVarianz, weaponVarianz);
        if (Random.Range(0f, 1f) < criticalChance) critMultiplier = 1.5f;
        else critMultiplier = 1;
        return (int)((meleeDMG + dmgVarianz) * critMultiplier);
    }
}
