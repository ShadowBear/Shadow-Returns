using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeWayPointsController : MonoBehaviour {

    public GameObject patrolPointParent;
    public Transform[] patrolPoints;
    
    [SerializeField]
    private bool[] freeWP;

    // Use this for initialization
    void Start () {
        patrolPointParent = gameObject;
        patrolPoints = patrolPointParent.GetComponentsInChildren<Transform>();
        freeWP = new bool[patrolPoints.Length];
        //first element is always false
        for (int i = 1; i < freeWP.Length; i++)
        {
            freeWP[i] = true;
        }
    }
	
	public void SetFree(int free,bool state)
    {
        freeWP[free] = state;
    }

    public bool getFreeState(int free)
    {
        return freeWP[free];
    }
}
