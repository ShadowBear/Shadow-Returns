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

    private int tapCounter = 0;
    private Coroutine singleTap;

    public Sprite swordSprite;
    public Sprite magicSprite;

    public Image actualSprite;


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
        //actualSprite = transform.GetComponentInChildren<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            tapCounter++;
            singleTap = StartCoroutine(SingleTap());
        }
    }

    IEnumerator SingleTap()
    {
        yield return new WaitForSeconds(0.3f);
        if (tapCounter >= 2 ) StartCoroutine(DoubleTap());
        else
        {
            //print("SingleTap");
            tapCounter = 0;
        }
    }

    IEnumerator DoubleTap()
    {
        StopCoroutine(singleTap);
        actualSprite.sprite = actualSprite.sprite == swordSprite ? magicSprite : swordSprite;
        playerAttack.rangeAttack = actualSprite.sprite == swordSprite ? false : true;
        //print("DoubleTap");
        tapCounter = 0;
        yield return null;
    }

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

        //Double or Single Tap to Change Weapon
        tapCounter++;
        singleTap = StartCoroutine(SingleTap());

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
