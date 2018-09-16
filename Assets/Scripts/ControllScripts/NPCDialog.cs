using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour {

    public string[] dialog;
    public string npcName;
    //public GameObject interactSymbol;
    private Canvas interactCanvasSymbol;


	// Use this for initialization
	public void Start () {
        interactCanvasSymbol = GetComponentInChildren<Canvas>();
        if(interactCanvasSymbol) interactCanvasSymbol.enabled = false;
	}


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(interactCanvasSymbol.enabled == false) interactCanvasSymbol.enabled = true;
            if (Input.GetButtonDown("Action"))
            {
                Interact();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactCanvasSymbol.enabled == true) interactCanvasSymbol.enabled = false;
        }
    }

    public virtual void Interact()
    {
        DialogSystem.Dialog.AddNewDialog(dialog, npcName);
    }
}
