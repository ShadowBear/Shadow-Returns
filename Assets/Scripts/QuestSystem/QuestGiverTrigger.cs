using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverTrigger : QuestGiver {

    private bool triggered = false;
    public GameObject enableParent;

    protected override void AssignQuest()
    {
        base.AssignQuest();
        if (AssignedQuest && !triggered)
        {
            triggered = true;
            if (enableParent) enableParent.SetActive(true);
        }
    }
}
