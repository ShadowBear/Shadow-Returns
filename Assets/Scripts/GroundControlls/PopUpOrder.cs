using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOrder : MoveMe {


    //public Vector3 posUp;
    //public Vector3 posDown;

    public bool correct = false;
    public CapsuleCollider trigger;

    // Use this for initialization
    void Start () {
        //positionUp = posUp;
        //positionDown = posDown;
        trigger = GetComponent<CapsuleCollider>();
	}
	
	//// Update is called once per frame
	//void Update () {
        
	//}

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
