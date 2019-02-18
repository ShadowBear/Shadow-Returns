﻿using System;
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
        shield.GetComponent<Shield>().ActivateShield();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shield.GetComponent<Shield>().DeActivateShield();
    }
}
