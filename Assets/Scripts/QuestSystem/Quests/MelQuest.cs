using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Mel´s key";
        QuestGiverName = "Mel";
        Description = "Finde Mel´s Schlüssel";
        ItemReward = reward;
        ExperienceReward = 100;

        Goals.Add(new CollectionGoal(this, "Key", "Collect 1 Key", false, 0, 1));
        Goals.ForEach(g => g.Init());
	}
}
