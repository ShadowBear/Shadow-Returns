using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotDamage : MonoBehaviour
{

    public GameObject explosion;
    public int damage = 10;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            //Apply Damage
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }

        if (!col.CompareTag("Enemy") && !col.CompareTag("Light") && !col.CompareTag("Boss") && !col.isTrigger)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
