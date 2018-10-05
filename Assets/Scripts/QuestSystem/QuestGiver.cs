using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPCDialog {

    //public bool AssignedQuest { get; set; }
    //public bool Helped { get; set; }
    public bool AssignedQuest;
    public bool Helped;

    //TalkStrings
    public string[] beforeQuestText;
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
                else DialogSystem.Dialog.AddNewDialog(new string[] { "Ich bin beschäftigt komm später wieder" }, npcName);
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
                if (beforeQuestText.Length > 0) DialogSystem.Dialog.AddNewDialog(beforeQuestText, npcName);
                else DialogSystem.Dialog.AddNewDialog(new string[] { "Jetzt ist nicht die Zeit zu reden komm später noch mal vorbei, vielleicht habe ich dann eine Aufgabe für dich." }, npcName);
            }
            else Interact();
        }
    }

    protected virtual void AssignQuest()
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
        else DialogSystem.Dialog.AddNewDialog(new string[] { "Ohh du hast anscheinend noch genug zu tun. Komm wieder wenn du ein paar deiner jetzigen Aufgaben erledigt hast." }, npcName);

    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Debug.Log("QuestDone");
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            GameManager.control.GetComponent<QuestLog>().RemoveQuest(Quest);
            if(getRewardText.Length > 0) DialogSystem.Dialog.AddNewDialog(getRewardText, npcName);
            else DialogSystem.Dialog.AddNewDialog(new string[] { "Danke das hier ist für dich!" }, npcName);
        }
        else
        {
            //DialogSystem.Dialog.AddNewDialog(new string[] { "What are you waiting for??", "Go Help!" }, npcName);
            if (onGoingText.Length > 0) DialogSystem.Dialog.AddNewDialog(onGoingText, npcName);
            else DialogSystem.Dialog.AddNewDialog(new string[] { "Worauf wartest du noch geh und hilf mir..." }, npcName);
        }
    }

    


}
