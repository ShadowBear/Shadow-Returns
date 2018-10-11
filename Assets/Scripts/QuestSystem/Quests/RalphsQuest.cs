using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RalphsQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "DrunkenNight";
        QuestGiverName = "Fischer Ralph";
        Description = "Finde Ralphs Waffen";
        ItemReward = reward;
        ExperienceReward = 70;

        Goals.Add(new CollectionGoal(this, "RalphsHammer", "Collect a Hammer", false, 0, 1));
        Goals.Add(new CollectionGoal(this, "RalphsShield", "Collect a Shield", false, 0, 1));
        Goals.ForEach(g => g.Init());
	}
}
