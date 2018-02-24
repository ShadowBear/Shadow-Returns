using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCheats : MonoBehaviour {


    private bool paused = false;
    public Transform libary;
    public Transform graveyard;
    public Transform start;
    private GameObject player;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = paused ? false : true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.GetComponent<HealthScript>().SetHealth(100);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = start.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = libary.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.transform.position = graveyard.transform.position;
        }

        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
