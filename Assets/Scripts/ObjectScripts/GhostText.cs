using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostText : MonoBehaviour {

    public GameObject canvas;
    public string dialogText;
    MenuController menu;

    void Start()
    {
        canvas.SetActive(false);
        menu = GameManager.control.GetComponent<MenuController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) canvas.SetActive(true);

    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player")) canvas.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
            {
                if (menu)
                {
                    menu.SetDialogText(dialogText);
                    menu.Dialog();
                }
            }
        }        
    }


}
