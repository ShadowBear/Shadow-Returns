using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRespawn : MonoBehaviour {

    public Transform respawnPos;
    public int fallDMG = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(respawnPos) other.transform.position = respawnPos.position;
            other.GetComponent<PlayerHealth>().TakeDamage(fallDMG);
        }
    }
}
