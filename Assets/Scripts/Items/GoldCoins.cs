using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour {

    public int coinValue = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.control.CollectCoins(coinValue);
            Destroy(this.gameObject);
        }
    }

    public void SetCoinValue(int value)
    {
        coinValue = value;
    }
}
