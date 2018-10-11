using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilaQuest : Quest {

    public GameObject reward;

    // Use this for initialization
    void Start()
    {
        QuestName = "Mila´s Amulett";
        QuestGiverName = "Mila";
        Description = "Besorg ihr Amulett";
        ItemReward = reward;
        ExperienceReward = 45;

        Goals.Add(new CollectionGoal(this, "Milas_Amulett", "Besorg ihr Amulett", false, 0, 1));
        Goals.ForEach(g => g.Init());
    }
}
