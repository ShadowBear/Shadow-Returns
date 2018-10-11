using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Healing : Item {

    public int healAmount;
    // Use this for initialization
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestLog.questLog.ItemCollected(ItemID);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().ConsumeHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
