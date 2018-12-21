using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item {

    new private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (antiPrell != 0) return;
            antiPrell++;
            GameManager.control.CollectKey();
            if(ItemID.Length > 0) QuestLog.questLog.ItemCollected(ItemID);
            else QuestLog.questLog.ItemCollected("Key");
            GameObject.FindGameObjectWithTag("ItemSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }


}
