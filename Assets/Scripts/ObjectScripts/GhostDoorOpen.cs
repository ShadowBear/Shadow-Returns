using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDoorOpen : MonoBehaviour {

    public GameObject[] keys;
    public GameObject gateKeeper;
    public GameObject chainBlock;

    private bool shallOpen = false;
    private int keysNeeded = 0;

    // Use this for initialization
	void Start () {
        keysNeeded = keys.Length;
	}
	
	// Update is called once per frame
	void Update () {
        if (shallOpen) OpenGate();

        keysNeeded = keys.Length;

        foreach (GameObject obj in keys)
        {
            if (obj.activeSelf) keysNeeded--; 
        }
        if (keysNeeded == 0) shallOpen = true;
	}

    void OpenGate()
    {
        gateKeeper.SetActive(false);
        chainBlock.SetActive(false);
        Destroy(gameObject);
    }
}
