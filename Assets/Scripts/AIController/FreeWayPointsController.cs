using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeWayPointsController : MonoBehaviour {

    public GameObject patrolPointParent;
    public Transform[] patrolPoints;

    //private int lastWP = 0;
    //private int nextWP = 0;

    [SerializeField]
    private bool[] freeWP;

    //public static FreeWayPointsController wayPointsController;

    //void Awake()
    //{
    //    //Unique GameManager
    //    if (wayPointsController == null) wayPointsController = this;
    //    else if (wayPointsController != this) Destroy(gameObject);
    //}


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
