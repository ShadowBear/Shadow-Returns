using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAmmu : MonoBehaviour {

    public GameObject explosion;
    public int damage = 10;
	
	void OnTriggerEnter(Collider col)
    {        
        if (col.CompareTag("Enemy"))
        {
            //Apply Damage
            col.GetComponent<HealthScript>().TakeDamage(damage);
        }
        if (col.CompareTag("Slime")) col.GetComponent<SlimeHealth>().TakeDamage(damage);
        if (!col.CompareTag("Player") && !col.CompareTag("Light"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
