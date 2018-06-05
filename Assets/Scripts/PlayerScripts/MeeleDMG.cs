using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleDMG : MonoBehaviour {

    public int damage = 35;
    public float pushForce = 3;
    public GameObject player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            //Apply Damage
            col.GetComponent<EnemyHealth>().TakeDamage(damage,true);
            if (col.GetComponent<Rigidbody>() != null)
                col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
        }
        if (col.CompareTag("Boss"))
        {
            //Apply Damage
            col.GetComponent<LibaryGhostBossHealth>().TakeDamage(damage);
            if (col.GetComponent<Rigidbody>() != null)
                col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
            
        }
        if (col.CompareTag("Slime")) {
            col.GetComponent<SlimeHealth>().TakeDamage(damage);
            if (col.GetComponent<Rigidbody>() != null)
                col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
        }
    }
}
