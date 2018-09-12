using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPCDialog {

    //public bool AssignedQuest { get; set; }
    //public bool Helped { get; set; }
    public bool AssignedQuest;
    public bool Helped;

    //TalkStrings
    public string[] afterCompletedText;
    public string[] getRewardText;
    public string[] onGoingText;

    public Quest questTypeToGive;

    //[SerializeField]
    //private GameObject quest;
    //[SerializeField]
    //private string questType;
    private Quest Quest { get; set; }


    new public void Start()
    {
        base.Start();
        AssignedQuest = false;
        Helped = false;
        print("QuestStart");
    }

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
            //DialogSystem.Dialog.AddNewDialog(new string[] { "Come back later i am busy now"}, npcName);
            DialogSystem.Dialog.AddNewDialog(afterCompletedText, npcName);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        //Takes the Quest as String -> KillQuest in questType
        //Quest = (Quest)quest.AddComponent(System.Type.GetType(questType));
        //GameManager.control.GetComponent<QuestLog>().AddQuest(questTypeToGive);
        QuestLog.questLog.AddQuest(questTypeToGive);
        Quest = questTypeToGive;
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            GameManager.control.GetComponent<QuestLog>().RemoveQuest(Quest);
            DialogSystem.Dialog.AddNewDialog(getRewardText, npcName);
        }
        else
        {
            //DialogSystem.Dialog.AddNewDialog(new string[] { "What are you waiting for??", "Go Help!" }, npcName);
            DialogSystem.Dialog.AddNewDialog(onGoingText, npcName);
        }
    }
}
