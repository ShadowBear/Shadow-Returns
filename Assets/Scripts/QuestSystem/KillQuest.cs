using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Butcher is comming";
        Description = "Kill a bunch of stuff.";
        ItemReward = reward;
        ExperienceReward = 100;

        Goals.Add(new KillGoal(this, 0, "Kill 3 Spiders", false, 0, 3));
        Goals.Add(new KillGoal(this, 1, "Kill 2 Magma", false, 0, 2));
        Goals.ForEach(g => g.Init());
	}
}
