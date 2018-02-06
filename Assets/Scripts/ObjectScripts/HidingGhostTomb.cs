using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingGhostTomb : MonoBehaviour {


    public GameObject ghost;
    public GraveyardController gyControll;
    public int hidingGhostHealth = 25;


    public void Die()
    {
        print("Tod");
        GameObject instGhost = Instantiate(ghost, transform.position, ghost.transform.rotation);
        instGhost.GetComponent<HealthScript>().SetHealth(hidingGhostHealth);
        if (gyControll != null) gyControll.ghostKilled();
    }
}
