using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceFlyToPlayer : MonoBehaviour {

    // Use this for initialization
    GameObject parent;
    Vector3 offset;
    public float forceSpeed = 0.08f;

	void Start () {
        parent = GetComponentInParent<Transform>().gameObject;
        offset = new Vector3(0, 1, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.transform.position += ((other.transform.position + offset) - parent.transform.position).normalized * forceSpeed;
            //print("Force");
            //GetComponentInChildren<Rigidbody>().AddForce(((other.transform.position + offset) - parent.transform.position).normalized * forceSpeed, ForceMode.Force);
        }
    }
}
