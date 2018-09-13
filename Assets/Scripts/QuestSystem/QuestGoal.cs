using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal : Goal {

    public string QuestID { get; set; }

    // Use this for initialization
    public QuestGoal(Quest quest, string questID, string description, bool completed)
    {
        this.Quest = quest;
        this.QuestID = questID;
        this.Description = description;
        this.Completed = completed;
    }

    public override void Init()
    {
        base.Init();
    }

    public override void QuestGoalCompleted(string quest)
    {
        if(QuestID == quest)
        {
            EvaluateQuest();
        }
    }
}
