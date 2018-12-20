using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour {

    [SerializeField]
    private string text;
    private int prell = 0;

    public string tutText = "Hinweis";

    MenuController menu;

    private void Start()
    {
        menu = GameManager.control.GetComponent<MenuController>();
    }

    // Use this for initialization
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (prell != 0) return;
            prell++;
            menu.SetDialogText(text);
            menu.SetHeaderText(tutText);
            menu.Dialog();
            Destroy(gameObject);            
        }
    }
}
