using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextControll : MonoBehaviour {


    private float lifeTime = 0.5f;
    private float scaleRate = 0.005f;
    private Vector3 scaleVector;
    private Vector3 rotationToCam;

    private Transform parentTrans;

    // Use this for initialization
    void Start () {
        scaleVector = new Vector3(scaleRate, scaleRate, scaleRate);
    }
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(transform.parent.gameObject);
        else
        {
            transform.localScale += scaleVector;
        }
    }
}
