using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item {

    new private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.control.CollectKey();
            if(ItemID.Length > 0) QuestLog.questLog.ItemCollected(ItemID);
            else QuestLog.questLog.ItemCollected("Key");
            Destroy(this.gameObject);
        }
    }


}
