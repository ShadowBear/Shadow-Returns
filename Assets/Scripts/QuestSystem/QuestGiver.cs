using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPCDialog {

    //public bool AssignedQuest { get; set; }
    //public bool Helped { get; set; }
    public bool AssignedQuest;
    public bool Helped;

    //TalkStrings
    public string[] beforQuestText;
    public string[] afterCompletedText;
    public string[] getRewardText;
    public string[] onGoingText;

    public Quest questTypeToGive;

    //[SerializeField]
    //private GameObject quest;
    //[SerializeField]
    //private string questType;
    private Quest Quest { get; set; }

    //Condition to be true before other Quest can start
    public string previousQuestID;
    private bool hasQuestActive = false;
    public bool needQuestToContinue;




    new public void Start()
    {
        base.Start();
        AssignedQuest = false;
        Helped = false;
        print("QuestStart");
    }

    public override void Interact()
    {
        if (!needQuestToContinue || hasQuestActive)
        {
            if (!AssignedQuest && !Helped)
            {
                //assign
                base.Interact();
                AssignQuest();
            }
            else if (AssignedQuest && !Helped)
            {
                //Check
                CheckQuest();
            }
            else
            {
                //Thanks for helped
                //DialogSystem.Dialog.AddNewDialog(new string[] { "Come back later i am busy now"}, npcName);
                if (afterCompletedText.Length > 0) DialogSystem.Dialog.AddNewDialog(afterCompletedText, npcName);
                else DialogSystem.Dialog.AddNewDialog(new string[] { "I am busy go away!" }, npcName);
            }
        }
        else
        {
            foreach (Quest q in QuestLog.questLog.activeQuests)
            {
                if (q.GetQuestID() == previousQuestID) hasQuestActive = true;
            }
            if (!hasQuestActive)
            {
                if (beforQuestText.Length > 0) DialogSystem.Dialog.AddNewDialog(beforQuestText, npcName);
                else DialogSystem.Dialog.AddNewDialog(new string[] { "This is not the time to talk come back later" }, npcName);
            }
            else Interact();
        }
    }

    void AssignQuest()
    {
        // 8 is the max amount of quests a player can store at a time.
        if(QuestLog.questLog.activeQuests.Count < 8)
        {
            AssignedQuest = true;
            //Takes the Quest as String -> KillQuest in questType
            //Quest = (Quest)quest.AddComponent(System.Type.GetType(questType));
            //GameManager.control.GetComponent<QuestLog>().AddQuest(questTypeToGive);
            QuestLog.questLog.AddQuest(questTypeToGive);
            Quest = questTypeToGive;
        }
        else DialogSystem.Dialog.AddNewDialog(new string[] { "Ohh you have already enough todo. Come back if u have done some of your other taks." }, npcName);

    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            GameManager.control.GetComponent<QuestLog>().RemoveQuest(Quest);
            if(getRewardText.Length > 0) DialogSystem.Dialog.AddNewDialog(getRewardText, npcName);
            else DialogSystem.Dialog.AddNewDialog(new string[] { "Thank you this is for you!" }, npcName);
        }
        else
        {
            //DialogSystem.Dialog.AddNewDialog(new string[] { "What are you waiting for??", "Go Help!" }, npcName);
            if (onGoingText.Length > 0) DialogSystem.Dialog.AddNewDialog(onGoingText, npcName);
            else DialogSystem.Dialog.AddNewDialog(new string[] { "What are you waiting for? Help me now!" }, npcName);
        }
    }
}
