using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGroundMaze : MonoBehaviour {



    //RiddleNumbers 1-4 erver Number stands for an direction
    // 1 - Up
    // 2 - Right
    // 3 - Down
    // 4 - Left

    //public int[] riddleNumbers;

    public CapsuleCollider[] triggerboxes;
    int triggerCounter;

    public GameObject objectToActivate;

    // Use this for initialization
	void Start () {
        triggerCounter = 0;
	}
	
    public bool NextTrigger(Collider trigger)
    {
        if(triggerCounter < triggerboxes.Length)
        {
            if (trigger == triggerboxes[triggerCounter])
            {
                triggerCounter++;
                if(triggerCounter == triggerboxes.Length)
                {
                    if (objectToActivate.GetComponent<BuildAreaUp>())
                    {
                        objectToActivate.GetComponent<BuildAreaUp>().BuildItUp();
                        ExitRiddle();
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for(int i = 0; i < triggerCounter; i++)
                {
                    if (trigger == triggerboxes[i]) return true;
                }
                triggerCounter = 0;
                foreach(CapsuleCollider cap in triggerboxes)
                {
                    if(cap.GetComponent<PopUpOrder>()) cap.GetComponent<PopUpOrder>().correct = false;
                }
                return false;
            }
        }
        return true;   
    }

    void ExitRiddle()
    {
        foreach (CapsuleCollider cap in triggerboxes)
        {
            if (cap.GetComponent<PopUpOrder>()) {
                cap.GetComponent<PopUpOrder>().correct = false;
                Destroy(cap.GetComponent<PopUpOrder>(),1f);
            }
        }
    }

}
