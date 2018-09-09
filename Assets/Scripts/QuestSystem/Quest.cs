using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour {

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    //Description max 30 Letters
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public GameObject ItemReward { get; set; }
    public bool Completed { get; set; }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        //Direct Reward -> Comment if Questgiver gives reward
        //if (Completed) GiveReward();
    }
    
    public void GiveReward()
    {
        if (ItemReward != null) print("Reward von Quest bekommen");
    }
}
