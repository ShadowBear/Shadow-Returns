using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Farm Job";
        QuestGiverName = "John Rübenkönig";
        Description = "Collect 10 Cabbages.";
        ItemReward = reward;
        ExperienceReward = 150;

        Goals.Add(new CollectionGoal(this, "Cabbage", "Collect 10 Cabbages", false, 0, 10));
        Goals.ForEach(g => g.Init());
	}
}
