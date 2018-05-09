using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAmmu : MonoBehaviour {

    public GameObject explosion;
    public int damage = 10;
    //Chance to Make Crit Values between 0-1f : 0 = 0%  and 1 = 100%
    public float criticalChance = 0.15f;
    private int critMultiplier = 0;
	
	void OnTriggerEnter(Collider col)
    {        
        if (col.CompareTag("Enemy") || col.CompareTag("Destroyable"))
        {
            //Apply Damage
            col.GetComponent<HealthScript>().TakeDamage(CalculateDmg());
        }
        if (col.CompareTag("Boss"))
        {
            col.GetComponent<LibaryGhostBossHealth>().TakeDamage(CalculateDmg());
        }
        if (col.CompareTag("Slime"))
        {            
            col.GetComponent<SlimeHealth>().TakeDamage(CalculateDmg());
        }
        if (!col.CompareTag("Player") && !col.CompareTag("Light") && !col.isTrigger)
        {
            if(explosion != null) Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    int CalculateDmg()
    {
        int dmgVarianz = Random.Range(1, 5) - 3;
        if (Random.Range(0f, 1f) < criticalChance) critMultiplier = 2;
        else critMultiplier = 1;
        return (damage + dmgVarianz) + ((critMultiplier * damage) - damage);
    }
}
