using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhilsQuest : Quest {

    public GameObject reward;

	// Use this for initialization
	void Start () {
        QuestName = "Ingredients for Phil";
        QuestGiverName = "Landarzt Phil";
        Description = "Besorge Phil´s Zutaten";
        ItemReward = reward;
        ExperienceReward = 150;

        Goals.Add(new CollectionGoal(this, "Pumpkin", "Collect 5 Pumpkins", false, 0, 5));
        Goals.Add(new CollectionGoal(this, "Shroom", "Collect 3 Shrooms", false, 0, 3));
        Goals.Add(new CollectionGoal(this, "Healflower", "Collect 12 Healflowers", false, 0, 12));
        Goals.ForEach(g => g.Init());
	}

    public override void GiveReward()
    {
        base.GiveReward();
        GetComponent<DialogQuestSetter>().enabled = true;
        GetComponent<QuestGiver>().enabled = false;
    }
}
