using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDamage : MonoBehaviour {

    public int dmgAmount = 10;
    

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player")){
            col.GetComponent<PlayerHealth>().TakeDamage(dmgAmount * Time.deltaTime);
        }
    }

}
