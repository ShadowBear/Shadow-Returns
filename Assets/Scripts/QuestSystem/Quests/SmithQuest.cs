using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Copper for the Smith";
        QuestGiverName = "Smith Bertha";
        Description = "Find 8 Copperblocks";
        ItemReward = reward;
        ExperienceReward = 200;

        Goals.Add(new CollectionGoal(this, "Copper", "Collect 8 Copperblock", false, 0, 8));
        Goals.ForEach(g => g.Init());
	}
}
