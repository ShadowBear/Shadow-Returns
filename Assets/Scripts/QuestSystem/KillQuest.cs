using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Butcher is comming";
        QuestGiverName = "Tomyliston";
        //Description max 30 Letters for Questlog
        Description = "Kill a bunch of stuff.";
        ItemReward = reward;
        ExperienceReward = 100;

        Goals.Add(new KillGoal(this, 0, "Kill 3 Spiders", false, 0, 3));
        Goals.Add(new KillGoal(this, 1, "Kill 2 Magma", false, 0, 2));
        Goals.Add(new CollectionGoal(this, "Pumpkin", "Collect a Pumpkin", false, 0, 1));
        Goals.ForEach(g => g.Init());
	}
}
