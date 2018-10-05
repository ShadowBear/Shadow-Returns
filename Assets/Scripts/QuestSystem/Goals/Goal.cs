using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal{

    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        //Default Init Stuff
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void EvaluateQuest()
    {
        Complete();        
    }

    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();
    }

    public virtual void EnemyDied(int EnemyID){ }

    public virtual void ItemCollected(string ItemID){ }

    public virtual void QuestGoalCompleted(string quest) { }
    public virtual void SpokenTo(string ID, bool done) { }

}
