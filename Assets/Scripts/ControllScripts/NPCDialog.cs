using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour {

    public string[] dialog;
    public string npcName;
    public bool reapeatSpeak = true;
    public string[] notRepeatText;
    private Canvas interactCanvasSymbol;

    [SerializeField]
    private bool forceSpeak = false;
    private bool AntiPrell = false;

    // Use this for initialization
    public void Start () {
        interactCanvasSymbol = GetComponentInChildren<Canvas>();
        if(interactCanvasSymbol) interactCanvasSymbol.enabled = false;
	}


    private void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (other.CompareTag("Player"))
        {
            if (interactCanvasSymbol.enabled == false) interactCanvasSymbol.enabled = true;
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetButtonDown("Action") && !AntiPrell)
            {
#elif UNITY_ANDROID
            if ((AndroidActionButton.androidActionButton.clicked ) && !AntiPrell)
            {
#endif
                AntiPrell = true;
                StartCoroutine(StopPrelling());
                Interact();
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

    public virtual void Interact()
    {
        if (dialog.Length > 0) DialogSystem.Dialog.AddNewDialog(dialog, npcName);
        else DialogSystem.Dialog.AddNewDialog(new string[] { "Was willst du geh weiter hier gibt es nichts was dich was angeht..." }, npcName);
        if (!reapeatSpeak)
        {
            if (notRepeatText.Length > 0) dialog = notRepeatText;
            else dialog = new string[] { "Lass mich, ich hab dir nichts mehr zusagen."};
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && forceSpeak)
        {
            forceSpeak = false;
            Interact();
        }
    }

    IEnumerator StopPrelling()
    {
        yield return new WaitForSeconds(0.5f);
        AntiPrell = false;
        yield return null;
    }
}
