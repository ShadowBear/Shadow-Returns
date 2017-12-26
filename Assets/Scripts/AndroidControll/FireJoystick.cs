using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FireJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image bgImage;
    private Image joystickImage;
    public Vector3 inputVector;
    private PlayerAndroidRotation playerAdRotation;
    private PlayerAttack playerAttack;

    // Use this for initialization

    void Awake()
    {
#if Unity_STANDALONE || UNITY_WEBPLAYER
        this.enabled = false;
#else
        this.enabled = true;
#endif
    }

    void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        playerAdRotation = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAndroidRotation>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>();
    }

    //void Update()
    //{
    //    if(Input.touchCount == 2)
    //    {
    //        print("Double Tap");
    //    }
    //}

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            //print("Pos X: " + pos.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);
            //print("Pos Y: " + pos.y);
            inputVector = new Vector3((pos.x * 2) - 1, 0, (pos.y * 2) - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Joystick Movement
            joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 2.5f), inputVector.z * (bgImage.rectTransform.sizeDelta.y / 2.5f));
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        playerAdRotation.onDrag = true;
        playerAttack.fireStickDown = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        playerAdRotation.onDrag = false;
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
        playerAttack.fireStickDown = false;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0) return inputVector.z;
        else return Input.GetAxis("Vertical");
    }

}
