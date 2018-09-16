using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Carrot Hunt";
        QuestGiverName = "Farmer Bill";
        Description = "Collect a bunch of stuff.";
        ItemReward = reward;
        ExperienceReward = 100;

        Goals.Add(new CollectionGoal(this, "Carrot", "Collect a Carrot", false, 0, 3));
        Goals.ForEach(g => g.Init());
	}
}
