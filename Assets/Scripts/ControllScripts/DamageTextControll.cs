using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextControll : MonoBehaviour {


    private float lifeTime = 0.5f;
    private float scaleRate = 0.001f;

    //TEST
    //private float lifeTime = 5.5f;
    //private float scaleRate = 0.0001f;


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
            //Vector3 rotationToCam = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
            //transform.GetComponent<Text>().transform.LookAt(rotationToCam);
            //transform.LookAt(Camera.main.transform);
            //print(transform.forward);
        }
    }
}
