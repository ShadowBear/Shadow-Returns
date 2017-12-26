using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldAndroidButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //TankMovement playerMovementScript;
    public GameObject shield;

    public void OnPointerDown(PointerEventData eventData)
    {
        shield.SetActive(true);
        //playerMovementScript.shieldedAndroid = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shield.SetActive(false);
        //playerMovementScript.shieldedAndroid = false;
    }

    // Use this for initialization
    void Start () {

        //shield = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shield>();
        //playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>();
    }
}
