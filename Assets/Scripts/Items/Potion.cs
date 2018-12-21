using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item {


    public AudioClip collectSound;
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
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(this.gameObject);
        }
    }

}
