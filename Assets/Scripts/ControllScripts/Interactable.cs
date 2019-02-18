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
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetButtonDown("Action") )
            {
#elif UNITY_ANDROID
            if (AndroidActionButton.androidActionButton.clicked ) 
            {
#endif
                Interact();
            }
        }
    }

}
