using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSword : MonoBehaviour {

    public bool collectSword = false;
    public bool collectGun = false;
    public bool collectShield = false;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Action"))
            {
                if (collectSword)
                {
                    GameManager.control.GetComponent<MenuController>().SetDialogText("You have found an Axe!!!");
                    GameManager.control.GetComponent<MenuController>().Dialog();
                    GameManager.control.SwordCollect();
                    Destroy(this.gameObject);
                }
                else if (collectGun) {
                    GameManager.control.GetComponent<MenuController>().SetDialogText("You have found a Gun!!!");
                    GameManager.control.GetComponent<MenuController>().Dialog();
                    GameManager.control.GunCollect();
                    Destroy(this.gameObject);
                }
                else if (collectShield) {
                    GameManager.control.GetComponent<MenuController>().SetDialogText("You have found a Shield Potion!!!");
                    GameManager.control.GetComponent<MenuController>().Dialog();
                    GameManager.control.ShieldCollect();
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
}
