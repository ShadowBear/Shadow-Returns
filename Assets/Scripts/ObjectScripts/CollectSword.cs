using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSword : MonoBehaviour {


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Action"))
            {
                GameManager.control.GetComponent<MenuController>().SetDialogText("You have found a Sword!!!");
                GameManager.control.GetComponent<MenuController>().Dialog();
                GameManager.control.SwordCollect();
                Destroy(this.gameObject);
            }
        }
    }
}
