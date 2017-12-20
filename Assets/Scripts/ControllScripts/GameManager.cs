using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int playerHealth = 100;
    public int shieldHealth = 100;

    public static GameManager control;
    
    
    // Use this for initialization
	void Awake () {
        //Unique GameManager
        if (control == null) control = this;
        else if (control != this) Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
