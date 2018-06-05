using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDamage : MonoBehaviour {

    public int dmgAmount = 10;


    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player")){
            //print("Dmg durch Licht");
            col.GetComponent<PlayerHealth>().TakeDamage(dmgAmount * Time.deltaTime);
        }
    }

}
