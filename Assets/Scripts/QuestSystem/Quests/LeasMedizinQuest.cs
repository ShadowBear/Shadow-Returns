using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeasMedizinQuest : Quest {

    public GameObject reward;

    // Use this for initialization
    void Start()
    {
        QuestName = "Leas Medizin";
        QuestGiverName = "Lea";
        Description = "Hole Medizin für Lea´s Tochter";
        ItemReward = reward;
        ExperienceReward = 95;

        Goals.Add(new DialogGoal(this, "Medizin_Lea", "Medizin for Lea", false, 0, 1));
        Goals.ForEach(g => g.Init());
    }
}
