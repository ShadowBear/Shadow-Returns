using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Item {

    private void Start()
    {
        ItemID = "Carrot";
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.control.playerHealth += 5;
            QuestLog.questLog.ItemCollected(ItemID);
            Destroy(this.gameObject);
        }
    }

}
