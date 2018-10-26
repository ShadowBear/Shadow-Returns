using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogGoal : Goal {
    public string DialogID { get; set; }
    public DialogGoal(Quest quest, string dialogID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.DialogID = dialogID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
    }

    public override void SpokenTo(string ID, bool done)
    {
        if(ID == DialogID)
        {
            if (done) RequiredAmount = CurrentAmount;
            //Debug.Log("Yeah ich hab mit xyz gesprochen");
            Evaluate();
        }
    }

}
