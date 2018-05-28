using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenWalls : MonoBehaviour {


    public GameObject wall;
    public GameObject secretCorridor;
    public string brokenWallText;
    private MenuController menu;



    private void Start()
    {
        menu = GameManager.control.GetComponent<MenuController>();
        secretCorridor.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Action") && Time.timeScale == 1)
            {
                //Time.timeScale = 0;
                menu.SetDialogText(brokenWallText);
                menu.Dialog();
                menu.dialogButton.onClick.AddListener(BreakIt);
            }
        }
    }

    public void BreakIt()
    {
        secretCorridor.SetActive(true);  
        wall.SetActive(false);
        //menu.Dialog();
        menu.dialogButton.onClick.RemoveListener(BreakIt);
        //Time.timeScale = 1;
    }

}
