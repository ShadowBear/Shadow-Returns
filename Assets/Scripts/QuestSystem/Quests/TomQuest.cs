using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomQuest : Quest {

    public GameObject reward;
    public int experienceReward;

	// Use this for initialization
	void Start () {
        QuestName = "Trouble at the ruine";
        QuestGiverName = "Tom";
        //Description max 30 Letters for Questlog
        Description = "Erkunde die Ruine";
        ItemReward = reward;
        if (experienceReward > 0) ExperienceReward = experienceReward;
        else ExperienceReward = 125;
        
        Goals.Add(new KillGoal(this, 96, "Kill all", false, 0, 2));
        Goals.ForEach(g => g.Init());
	}
}
