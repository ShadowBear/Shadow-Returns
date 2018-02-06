using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRoomController : MonoBehaviour {

    // Use this for initialization

    public Animator bookshelf1;
    public Animator bookshelf2;
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bookshelf1.SetBool("open", true);
            bookshelf2.SetBool("open", true);
        }
    }


}
