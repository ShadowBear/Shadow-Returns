using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaUp : MonoBehaviour {

    public Transform[] objectsToMove;
    public bool resetable = false;
    public bool reverse = false;
    public bool isTrigger = false;
    public GameObject triggerObject;

    // Use this for initialization
    void Start()
    {
        objectsToMove = GetComponentsInChildren<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BuildItUp();
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && resetable)
        {
            foreach (Transform child in objectsToMove)
            {
                if (child.transform.GetComponent<MoveMe>())
                {
                    child.transform.GetComponent<MoveMe>().moveDown = reverse ? false : true; 
                    child.transform.GetComponent<MoveMe>().moveUp = reverse ? true : false;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && resetable)
        {
            if (isTrigger)
            {
                if (triggerObject.GetComponent<GroundMoving>()) triggerObject.GetComponent<GroundMoving>().activateIt = true;
            }
        }
    }


    public void BuildItUp()
    {
        foreach (Transform child in objectsToMove)
        {
            if (child.transform.GetComponent<MoveMe>())
            {
                child.transform.GetComponent<MoveMe>().moveDown = reverse ? true : false;
                child.transform.GetComponent<MoveMe>().moveUp = reverse ? false : true;
            }
        }
    }

}
