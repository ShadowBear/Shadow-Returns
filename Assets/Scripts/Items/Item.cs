using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    protected string ItemID;

    protected int antiPrell = 0;

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (antiPrell != 0) return;
            antiPrell++;
            QuestLog.questLog.ItemCollected(ItemID);
            Destroy(gameObject);
        }
    }
    


}
