using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour {


    public int experience;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            GameManager.control.ReceiveExperience(experience);
            print("Zerstört");
            //Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }

}
