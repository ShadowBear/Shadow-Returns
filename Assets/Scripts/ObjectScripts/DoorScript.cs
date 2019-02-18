﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {


    private Animator anim;
    public bool keyNeededBool = false;
    public bool triggersEnemys = false;

    public bool doorStatus = false;

    private EnemyInstantiateTrigger enemyTrigger;

    public string keyNeededText;

    public AudioClip doorSound;

    private int AntiPrell = 0;

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        enemyTrigger = GetComponent<EnemyInstantiateTrigger>();
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetButtonDown("Action") && AntiPrell == 0)
            {
#elif UNITY_ANDROID
            if ((AndroidActionButton.androidActionButton.clicked ) && AntiPrell == 0)
            {
#endif
                AntiPrell++;
                StartCoroutine(StopPrelling());
                GameManager.control.GetComponent<MenuController>().SetHeaderText("Tür");
                //Debug.Log("ActionToDo");
                if (keyNeededBool)
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
                        if (keyNeededText.Length > 0) GameManager.control.GetComponent<MenuController>().SetDialogText(keyNeededText);
                        else GameManager.control.GetComponent<MenuController>().SetDialogText("Ohne Schlüssel wird das nichts!");
                        GameManager.control.GetComponent<MenuController>().Dialog();
                    }
                }else
                {
                    doorStatus = anim.GetBool("Open")? false: true;
                    anim.SetBool("Open", doorStatus);
                    if(doorSound != null) AudioSource.PlayClipAtPoint(doorSound, transform.position);
                    if (triggersEnemys) {
                        enemyTrigger.Triggered();
                        triggersEnemys = false;
                    }
                }         
            }            
        }
    }


    IEnumerator StopPrelling()
    {
        yield return new WaitForSeconds(1f);
        AntiPrell = 0;
    }


}
