using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectedTut : MonoBehaviour {

    [SerializeField]
    private string TutorialText;
    int prell = 0;
    public string headText = "Hinweis";
    MenuController menu;

    private void Start()
    {
        menu = GameManager.control.GetComponent<MenuController>();
    }

    // Use this for initialization
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.control.keyNmbr > 0)
            {
                if (prell != 0) return;
                prell++;
                menu.SetDialogText(TutorialText);
                menu.SetHeaderText(headText);
                menu.Dialog();

                Destroy(gameObject);
            }
        }
    }
}
