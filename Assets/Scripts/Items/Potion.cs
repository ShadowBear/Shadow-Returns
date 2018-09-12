using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item {
    
    private void Start()
    {
        ItemID = "Heal_Potion";
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.control.CollectPotion();
            QuestLog.questLog.ItemCollected(ItemID);
            Destroy(this.gameObject);
        }
    }

}
