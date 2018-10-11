using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SvensonQuest : Quest {

    public GameObject reward;
    public int experienceReward;

	// Use this for initialization
	void Start () {
        QuestName = "Butcher is comming";
        QuestGiverName = "Svenson";
        //Description max 30 Letters for Questlog
        Description = "Ruhe hinterm Haus";
        ItemReward = reward;
        if (experienceReward > 0) ExperienceReward = experienceReward;
        else ExperienceReward = 25;

        //Goals.Add(new KillGoal(this, 11, "Kill 3 Spiders", false, 0, 3));
        //Goals.Add(new KillGoal(this, 4, "Kill 2 Magma", false, 0, 2));
        //Goals.Add(new KillGoal(this, 2, "Kill 4 Bug", false, 0, 4));
        Goals.Add(new KillGoal(this, 98, "Kill all", false, 0, 9));
        Goals.ForEach(g => g.Init());
	}
}
