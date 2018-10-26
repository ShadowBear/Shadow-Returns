using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidButtons : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{


    public bool shallShoot = false;


    public void OnPointerUp(PointerEventData eventData)
    {
        //if(shootingScript.m_Fired == false) shootingScript.Fire();
        shallShoot = false;        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        shallShoot = true;
        //shootingScript.m_Fired = false;
    }
}
