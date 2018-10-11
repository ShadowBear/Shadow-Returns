using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Copper for the Smith";
        QuestGiverName = "Schmied Bertha";
        Description = "Finde 8 Kupferbarren";
        ItemReward = reward;
        ExperienceReward = 200;

        Goals.Add(new CollectionGoal(this, "Copper", "Collect 8 Copperblock", false, 0, 8));
        Goals.ForEach(g => g.Init());
	}
}
