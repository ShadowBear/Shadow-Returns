using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogTrigger : NPCDialog {

    public GameObject enableParent;
    private bool enabledTrigger = false;
    public bool needQuest;
    public string questNeededID;
    public string[] beforeQuestString;
    private bool hasQuest = false;

    // Use this for initialization
    public override void Interact()
    {
        if (!needQuest)
        {
            base.Interact();
            if (enableParent && !enabledTrigger) enableParent.SetActive(true);
        }
        else
        {
            foreach (Quest q in QuestLog.questLog.activeQuests)
            {
                if (q.GetQuestID() == questNeededID) hasQuest = true;
            }
            if (hasQuest)
            {
                base.Interact();
                if (enableParent && !enabledTrigger) enableParent.SetActive(true);
            }
            else
            {
                if (beforeQuestString.Length > 0) DialogSystem.Dialog.AddNewDialog(beforeQuestString, npcName);
                else DialogSystem.Dialog.AddNewDialog(new string[] { "Ich bin noch nicht bereit dazu..." }, npcName);
            }
        }
        

    }
}
