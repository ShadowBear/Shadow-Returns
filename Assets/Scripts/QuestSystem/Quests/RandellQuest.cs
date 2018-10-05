using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandellQuest : Quest {

    public GameObject reward;

    // Use this for initialization
    void Start()
    {
        QuestName = "IntoTheCastle";
        QuestGiverName = "Randell";
        Description = "Hilf den Bürgern";
        ItemReward = reward;
        ExperienceReward = 500;

        Goals.Add(new QuestGoal(this, "LeaNeedHelp", "Help Lea", false));
        Goals.Add(new QuestGoal(this, "TomRuinen", "Help Tom", false));
        Goals.Add(new QuestGoal(this, "SvensonNeedHelp", "Help Svenson", false));
        Goals.Add(new QuestGoal(this, "ThormundsChallenge", "Help Thormund", false));
        Goals.Add(new QuestGoal(this, "MillasAmulett", "Help Milla", false));
        Goals.Add(new QuestGoal(this, "QuietPls", "Help Ben", false));
        Goals.ForEach(g => g.Init());
    }
}
