using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoving : MonoBehaviour {

    public float maxTime = 5f;
    private float timer;
    public Transform[] objectsToMove;
    [SerializeField]
    private bool active;
    public bool activateIt;
    public bool moving = false;



    // Use this for initialization
    void Start () {
        timer = 0;
        active = false;
        objectsToMove = GetComponentsInChildren<Transform>();
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Reset();
                active = false;
            }
        }
        if (activateIt)
        {
            timer = maxTime;
            Activate();
            activateIt = false;
            active = true;
        }
	}

    void Activate()
    {
        foreach (Transform child in objectsToMove)
        {
            if (child.transform.GetComponent<MoveMe>())
            {
                child.transform.GetComponent<MoveMe>().moveDown = false;
                child.transform.GetComponent<MoveMe>().moveUp = true;
            }            
        }
    }

    void Reset()
    {
        foreach (Transform child in objectsToMove)
        {
            if (child.transform.GetComponent<MoveMe>())
            {
                child.transform.GetComponent<MoveMe>().moveUp = false;
                child.transform.GetComponent<MoveMe>().moveDown = true;
            }
        }
    }



}
