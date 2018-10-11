using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenQuest : Quest {

    public GameObject reward;
    public int experienceReward;

	// Use this for initialization
	void Start () {
        QuestName = "QuietPls";
        QuestGiverName = "Ben";
        //Description max 30 Letters for Questlog
        Description = "Erlöse den Friedhof ";
        ItemReward = reward;
        if (experienceReward > 0) ExperienceReward = experienceReward;
        else ExperienceReward = 80;

        Goals.Add(new KillGoal(this, 97, "Kill all", false, 0, 13));
        Goals.ForEach(g => g.Init());
	}
}
