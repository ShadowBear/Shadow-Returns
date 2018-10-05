using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherWillQuest : Quest {

    public GameObject reward;

    // Use this for initialization
    void Start()
    {
        QuestName = "Will´s Son";
        QuestGiverName = "Will";
        Description = "Will vermisst seinen Sohn";
        ItemReward = reward;
        ExperienceReward = 65;

        Goals.Add(new DialogGoal(this, "Wills_Son", "Find Will´s son", false, 0, 1));
        Goals.ForEach(g => g.Init());
    }
}
