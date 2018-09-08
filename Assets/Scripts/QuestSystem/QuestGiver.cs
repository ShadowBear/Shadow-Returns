using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPCDialog {

    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    //TalkStrings
    public string[] afterCompletedText;
    public string[] getRewardText;
    public string[] onGoingText;

    [SerializeField]
    private GameObject quest;
    [SerializeField]
    private string questType;
    private Quest Quest { get; set; }

    
    public override void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            //assign
            base.Interact();
            AssignQuest();
        }else if(AssignedQuest && !Helped)
        {
            //Check
            CheckQuest();
        }
        else
        {
            //Thanks for helped
            DialogSystem.Dialog.AddNewDialog(new string[] { "Come back later i am busy now"}, name);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        //Takes the Quest as String -> KillQuest in questType
        Quest = (Quest)quest.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            DialogSystem.Dialog.AddNewDialog(new string[] { "Thanks for that Here´s your reward.", "More Dialog" }, name);
        }
        else
        {
            DialogSystem.Dialog.AddNewDialog(new string[] { "What are you waiting for??", "Go Help!" }, name);
        }
    }
}
