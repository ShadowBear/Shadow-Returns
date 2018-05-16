using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {


    private Animator anim;
    public bool keyNeededBool = false;
    public bool specialKey = false;

    public bool doorStatus = false;

    public GameObject specialKeyObject;

    public string keyNeededText;
    public string swordNeededText;

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Action"))
            {
                if (!keyNeededBool)
                {
                    doorStatus = anim.GetBool("Open")? false: true;
                    anim.SetBool("Open", doorStatus);
                }
                else if (!specialKey)
                {
                    if (GameManager.control.keyNmbr > 0)
                    {
                        GameManager.control.UseKey();
                        anim.SetBool("Open", true);
                    }
                }
                else
                {
                    if (GameManager.control.keyNmbr > 0)
                    {
                        if (GameManager.control.swordCollected)
                        {
                            GameManager.control.UseKey();
                            anim.SetBool("Open", true);
                        }
                        else
                        {
                            GameManager.control.GetComponent<MenuController>().SetDialogText(swordNeededText);
                            GameManager.control.GetComponent<MenuController>().Dialog();
                        }
                    }
                    else
                    {
                        GameManager.control.GetComponent<MenuController>().SetDialogText(keyNeededText);
                        GameManager.control.GetComponent<MenuController>().Dialog();
                    }

                }
            }
            
        }
    }
}
