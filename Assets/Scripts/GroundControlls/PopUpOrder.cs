using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOrder : MoveMe {

     public bool correct = false;
    public CapsuleCollider trigger;

    // Use this for initialization
    void Start () {
        trigger = GetComponent<CapsuleCollider>();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (GetComponentInParent<PopUpGroundMaze>().NextTrigger(trigger)) correct = true;
            else correct = false;
        }
        if (correct)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    public void ExitRiddle()
    {
        if (correct)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

}
