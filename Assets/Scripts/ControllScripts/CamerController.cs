using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {

    public GameObject follow;
    public Vector3 offset;
    public float smoothSpeed = .125f;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    //private RaycastHit oldHit;

    // Use this for initialization
    void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        desiredPosition = follow.transform.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        //XRay();
    }
    //private void XRay()
    //{

    //    float characterDistance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
    //    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, fwd, out hit, characterDistance))
    //    {
    //        if (oldHit.transform)
    //        {

    //            // Add transparence
    //            Color colorA = oldHit.transform.gameObject.GetComponent<Renderer>().material.color;
    //            colorA.a = 1f;
    //            oldHit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorA);
    //        }

    //        // Add transparence
    //        Color colorB = hit.transform.gameObject.GetComponent<Renderer>().material.color;
    //        colorB.a = 0.5f;
    //        hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorB);

    //        // Save hit
    //        oldHit = hit;
    //    }
    //}
}

