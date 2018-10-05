using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogQuestSetter : NPCDialog {

    private bool dialogQuestCompleted = false;
    public string dialogID;

    public override void Interact()
    {
        base.Interact();
        if (!dialogQuestCompleted)
        {
            dialogQuestCompleted = true;
            QuestLog.questLog.DialogDone(dialogID, true);
        }
    }

}
