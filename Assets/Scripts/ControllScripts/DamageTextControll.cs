using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextControll : MonoBehaviour {


    private float lifeTime = 0.5f;
    private float scaleRate = 0.001f;

    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
        else
        {
            transform.localScale += new Vector3(scaleRate, scaleRate, scaleRate);
        }
	}
}
