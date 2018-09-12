using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    protected string ItemID;

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            QuestLog.questLog.ItemCollected(ItemID);
            Destroy(gameObject);
        }
    }
    


}
