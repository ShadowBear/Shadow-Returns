using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour {

    DoorScript doorScript;
    public TrapLever[] traps;
    int trapCounter = 0;
    int trapMax;
    
    // Use this for initialization
	void Start () {
        doorScript = GetComponent<DoorScript>();
        trapMax = traps.Length;
        trapCounter = trapMax;
    }
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            foreach (TrapLever trap in traps)
            {
                if (trap.isActivated == false) trapCounter--;
            }
            if (trapCounter == 0) doorScript.keyNeededBool = false;
            else trapCounter = trapMax;
        }
	}
}
