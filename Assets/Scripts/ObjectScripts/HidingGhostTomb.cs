using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingGhostTomb : MonoBehaviour {


    public GameObject ghost;
    public GraveyardController gyControll;
    public int hidingGhostHealth = 25;


    public void Die()
    {
        GameObject instGhost = Instantiate(ghost, transform.position, ghost.transform.rotation);
        instGhost.GetComponent<HealthScript>().SetHealth(hidingGhostHealth);
        if (gyControll != null) gyControll.GhostKilled();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammu"))
        {

        }
    }
}
