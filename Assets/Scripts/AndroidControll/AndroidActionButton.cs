using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidActionButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public static AndroidActionButton androidActionButton;
    public bool clicked = false;

    private void Start()
    {
        if (androidActionButton == null) androidActionButton = this;
        else if (androidActionButton != this) Destroy(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;
    }
}
