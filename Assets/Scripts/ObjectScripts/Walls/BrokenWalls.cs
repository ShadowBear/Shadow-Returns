using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenWalls : MonoBehaviour {


    public GameObject wall;
    public GameObject secretCorridor;
    public string brokenWallText;
    private MenuController menu;

    private Canvas interactCanvasSymbol;

    private void Start()
    {
        menu = GameManager.control.GetComponent<MenuController>();
        secretCorridor.SetActive(false);
        interactCanvasSymbol = GetComponentInChildren<Canvas>();
        if (interactCanvasSymbol) interactCanvasSymbol.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactCanvasSymbol.enabled == false) interactCanvasSymbol.enabled = true;
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetButtonDown("Action") && Time.timeScale == 1)
            {
#elif UNITY_ANDROID
            if ((AndroidActionButton.androidActionButton.clicked ) && Time.timeScale == 1)
            {
#endif
                menu.SetDialogText(brokenWallText);
                menu.SetHeaderText("Rissige Wand");
                menu.Dialog();
                menu.dialogButton.onClick.AddListener(BreakIt);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;
        if (other.CompareTag("Player"))
        {
            if (interactCanvasSymbol.enabled == true) interactCanvasSymbol.enabled = false;
        }
    }

    public void BreakIt()
    {
        secretCorridor.SetActive(true);  
        wall.SetActive(false);
        menu.dialogButton.onClick.RemoveListener(BreakIt);

        if (interactCanvasSymbol.enabled == true) interactCanvasSymbol.enabled = false;
        Destroy(this);
    }

}
