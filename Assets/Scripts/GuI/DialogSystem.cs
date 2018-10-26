using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public static DialogSystem Dialog;
    public string npcName;
    public GameObject dialogPanel;
    public List<string> dialogLines = new List<string>();

    Button continueBtn;
    Text dialogText;
    Text npcNameText;
    int dialogIndex;

    private void Awake()
    {
        if (Dialog != null && Dialog != this)Destroy(gameObject);
        else Dialog = this;

        continueBtn = dialogPanel.transform.GetChild(0).transform.Find("Image").GetComponentInChildren<Button>();
        continueBtn.onClick.AddListener(delegate { ContinueDialog(); });
        dialogText = dialogPanel.transform.GetChild(0).transform.Find("DialogText").GetComponent<Text>();
        npcNameText = dialogPanel.transform.GetChild(0).transform.Find("Image").transform.Find("NPCName").GetComponentInChildren<Text>();
        
        dialogPanel.SetActive(false);
    }


    public void AddNewDialog(string [] lines, string npcName)
    {
        dialogIndex = 0;
        dialogLines = new List<string>();
        dialogLines.AddRange(lines);
        this.npcName = npcName;
        CreateDialog();
        //Debug.Log(dialogLines.Count);
    }

    public void CreateDialog()
    {
        dialogText.text = dialogLines[dialogIndex];
        npcNameText.text = npcName;
        dialogPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueDialog()
    {
        if (dialogIndex < dialogLines.Count - 1)
        {
            dialogIndex++;
            dialogText.text = dialogLines[dialogIndex];
        }
        else
        {
            dialogPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
