using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleDMG : MonoBehaviour {


    //************** OLD CLASS *******************//


    //public int damage = 35;
    //public float pushForce = 3;
    //private GameObject player;

    //private void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.GetComponent<ItemHealth>())
    //    {
    //        Debug.Log("ItemDamage");
    //        col.GetComponent<ItemHealth>().TakeDamage(damage, true);
    //        if (col.GetComponent<Rigidbody>() != null)
    //            col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
    //    }
    //    if (col.CompareTag("Enemy"))
    //    {
    //        //Apply Damage
    //        Debug.Log("EnemyMeele");
    //        col.GetComponent<EnemyHealth>().TakeDamage(damage,true);
    //        if (col.GetComponent<Rigidbody>() != null)
    //            col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
    //    }
    //    if (col.CompareTag("Boss"))
    //    {
    //        //Apply Damage
    //        col.GetComponent<LibaryGhostBossHealth>().TakeDamage(damage);
    //        if (col.GetComponent<Rigidbody>() != null)
    //            col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
            
    //    }
    //    if (col.CompareTag("Slime")) {
    //        col.GetComponent<SlimeHealth>().TakeDamage(damage);
    //        if (col.GetComponent<Rigidbody>() != null)
    //            col.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.forward) * pushForce, ForceMode.Impulse);
    //    }
    //}
}
