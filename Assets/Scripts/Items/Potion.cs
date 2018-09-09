using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

    string ItemID = "Heal_Potion";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.control.CollectPotion();
            //if (GameObject.FindGameObjectWithTag("Quest").GetComponent<Quest>())
            //{
            //    foreach(Quest q in GameObject.FindGameObjectWithTag("Quest").GetComponents<Quest>())
            //    {
            //        //Solo Quest
            //        //foreach (CollectionGoal g in GameObject.FindGameObjectWithTag("Quest").GetComponent<Quest>().Goals)
                    
            //        //Multiple Quests
            //        foreach(Goal g in q.Goals)
            //        {
            //            g.ItemCollected(ItemID);
            //        }
            //    }
                
            //}

            if (GameObject.FindGameObjectWithTag("Quest"))
            {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Quest"))
                {
                    if (go.GetComponent<KillQuest>())
                    {
                        foreach (Goal g in go.GetComponent<KillQuest>().Goals)
                        {
                            g.ItemCollected(ItemID);
                        }
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }

}
