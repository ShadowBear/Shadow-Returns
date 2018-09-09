using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public virtual void Interact()
    {
        Debug.Log("Interact Stuff");
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Action"))
            {
                Interact();
            }
        }
    }

}
