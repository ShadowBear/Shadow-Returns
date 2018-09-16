using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour {

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string QuestGiverName { get; set; }
    //Description max 30 Letters
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public GameObject ItemReward { get; set; }
    public bool Completed { get; set; }

    private float explosionRadius = 5;
    private int explosionForce = 250;
    [SerializeField]
    private string QuestID;

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        //Direct Reward -> Comment if Questgiver gives reward
        //if (Completed) GiveReward();
    }
    
    public void GiveReward()
    {
        GameManager.control.ReceiveExperience(ExperienceReward);
        QuestLog.questLog.QuestCompleted(QuestID);
        if (ItemReward != null)
        {
            print("Reward von Quest bekommen");
            GameObject rewardObject = Instantiate(ItemReward, transform.position + new Vector3(0,1,0), transform.rotation);
            if (rewardObject.GetComponent<Rigidbody>())
            {
                Vector3 randomExplosionVector = new Vector3(0, 0.9f, 0.5f);
                rewardObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position + randomExplosionVector, explosionRadius);
            }
        }
    }

    public string GetQuestID()
    {
        return QuestID;
    }
}
