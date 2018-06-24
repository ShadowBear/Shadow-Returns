using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {


    private Animator anim;
    public bool keyNeededBool = false;
    public bool specialKey = false;
    public bool triggersEnemys = false;

    public bool doorStatus = false;

    public GameObject specialKeyObject;
    private EnemyInstantiateTrigger enemyTrigger;

    public string keyNeededText;
    public string swordNeededText;

    public AudioClip doorSound;

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        enemyTrigger = GetComponent<EnemyInstantiateTrigger>();
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
                    if(doorSound != null) AudioSource.PlayClipAtPoint(doorSound, transform.position);
                    if (triggersEnemys) {
                        enemyTrigger.Triggered();
                        triggersEnemys = false;
                    }
                }
                else if (!specialKey)
                {
                    if (GameManager.control.keyNmbr > 0)
                    {
                        GameManager.control.UseKey();
                        anim.SetBool("Open", true);
                        keyNeededBool = false;
                        if (doorSound != null) AudioSource.PlayClipAtPoint(doorSound, transform.position);
                        if (triggersEnemys)
                        {
                            enemyTrigger.Triggered();
                            triggersEnemys = false;
                        }
                    }
                    else
                    {
                        GameManager.control.GetComponent<MenuController>().SetDialogText(keyNeededText);
                        GameManager.control.GetComponent<MenuController>().Dialog();
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
                            specialKey = false;
                            keyNeededBool = false;
                            if (doorSound != null) AudioSource.PlayClipAtPoint(doorSound, transform.position);
                            if (triggersEnemys)
                            {
                                enemyTrigger.Triggered();
                                triggersEnemys = false;
                            }
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
