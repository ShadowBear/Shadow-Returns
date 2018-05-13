using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextControll : MonoBehaviour {


    private float lifeTime = 0.5f;
    private float scaleRate = 0.001f;
    private Vector3 scaleVector;
    private Vector3 rotationToCam;
    private Transform cameraTrans;

    //TEST
    //private float lifeTime = 5.5f;
    //private float scaleRate = 0.0001f;

    private Transform parentTrans;
    public RectTransform itselfTrans;


    // Use this for initialization
    void Start () {
        parentTrans = transform.parent;
        scaleVector = new Vector3(scaleRate, scaleRate, scaleRate);
        cameraTrans = Camera.main.transform;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
        else
        {
            transform.localScale += scaleVector;
            rotationToCam.Set(cameraTrans.position.x, 0, cameraTrans.position.z);
            parentTrans.LookAt(rotationToCam);
            parentTrans.forward = -parentTrans.forward;
            itselfTrans.forward = parentTrans.forward;
        }
    }
}
