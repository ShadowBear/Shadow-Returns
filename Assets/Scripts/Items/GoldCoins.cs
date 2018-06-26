using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour {

    public int coinValue = 5;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.control.CollectCoins(coinValue);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetCoinValue(int value)
    {
        coinValue = value;
    }
}
