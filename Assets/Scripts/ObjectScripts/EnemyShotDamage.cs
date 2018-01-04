﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotDamage : MonoBehaviour
{

    public GameObject explosion;
    public int damage = 10;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //Apply Damage
            col.GetComponent<HealthScript>().TakeDamage(damage);
        }

        if (!col.CompareTag("Enemy") && !col.CompareTag("Light"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
