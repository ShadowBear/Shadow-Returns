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
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetButtonDown("Action"))
            {
#elif UNITY_ANDROID
            if (AndroidActionButton.androidActionButton.clicked )
            {
#endif
                if (collectSword)
                {
                    GameManager.control.GetComponent<MenuController>().SetDialogText("Wow du hast eine Waffe gefunden!!!");
                    GameManager.control.GetComponent<MenuController>().Dialog();
                    //GameManager.control.SwordCollect();
                    Destroy(this.gameObject);
                }
                else if (collectGun) {
                    GameManager.control.GetComponent<MenuController>().SetDialogText("Wow du hast eine Waffe gefunden!!!");
                    GameManager.control.GetComponent<MenuController>().Dialog();
                    //GameManager.control.GunCollect();
                    Destroy(this.gameObject);
                }
                else if (collectShield) {
                    GameManager.control.GetComponent<MenuController>().SetDialogText("Wow du hast einen Schildtrank gefunden!!!");
                    GameManager.control.GetComponent<MenuController>().Dialog();
                    //GameManager.control.ShieldCollect();
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
}
