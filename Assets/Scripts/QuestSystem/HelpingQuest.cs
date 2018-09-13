using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpingQuest : Quest {

    public GameObject reward;

    // Use this for initialization
    void Start()
    {
        QuestName = "HelpingQuest";
        QuestGiverName = "QuestHelper";
        Description = "Help these 2 Douchebags";
        ItemReward = reward;
        ExperienceReward = 500;

        Goals.Add(new QuestGoal(this, "CarrotQuest", "Help Carrot", false));
        Goals.Add(new QuestGoal(this, "KillQuest", "Kill All", false));
        Goals.ForEach(g => g.Init());
    }
}
