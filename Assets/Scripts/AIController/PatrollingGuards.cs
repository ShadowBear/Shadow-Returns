using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingGuards : MonoBehaviour {


    public GameObject patrolPointParent;
    public Transform[] patrolPoints;
    private float guardingTime = 0f;
    public float minGuardingTime = 5f;
    public float maxGuardingTime = 10f;

    //public FreeWayPointsController wayPointController;


    private NavMeshAgent navMeshAgent;
    private NavMeshObstacle navMeshObstacle;
    private Transform nextTarget;
    private Transform currentTarget;

    private int lastWP = 0;
    private int nextWP = 0;

    [SerializeField]
    private State currentState;
    private Animator animator;
    private int idleAnimation;
    private bool idleSet = false;
    [SerializeField]
    //private bool[] freeWP;

    enum State
    {
        Guarding, Patrolling
    };

    // Use this for initialization
    void Start()
    {
        patrolPoints = patrolPointParent.GetComponentsInChildren<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        //wayPointController = GetComponentInParent<FreeWayPointsController>();
        currentState = State.Guarding;
        animator = GetComponent<Animator>();
        animator.SetBool("Static_b", false);        
    }


	// Update is called once per frame
	void Update () {
        //Obstacle Avoid Off Nav On
        //navMeshObstacle.enabled = false;
        //navMeshAgent.enabled = true;
        if (currentState == State.Guarding) {

            if (guardingTime <= 0) NextGuardPoint(false);
            else
            {
                guardingTime -= Time.deltaTime;
                transform.localEulerAngles = new Vector3(nextTarget.transform.eulerAngles.x, nextTarget.transform.eulerAngles.y+90, nextTarget.transform.eulerAngles.z);                
            }      
        }else if(currentState == State.Patrolling)
        {
            if(navMeshAgent.remainingDistance < 2.0f && navMeshAgent.remainingDistance >= 0.5f)
            {
                if (!FreeWayPointsController.wayPointsController.getFreeState(nextWP)) NextGuardPoint(true);
            }
            else if (navMeshAgent.remainingDistance < 0.5f)
            {
                //if (!FreeWayPointsController.wayPointsController.getFreeState(nextWP))NextGuardPoint(true);
                //else
                //{
                    currentState = State.Guarding;
                    navMeshAgent.isStopped = true;
                //}
            }
            else navMeshAgent.isStopped = false;
        }

        if (!navMeshAgent.isStopped)
        {
            animator.SetFloat("Speed_f", 0.3f);
            //Set Idle Back to 0 for Walk Anim work properly
            animator.SetInteger("Animation_int", 0);
        }
        else
        {
            animator.SetFloat("Speed_f", 0.0f);
            if (!idleSet && currentState == State.Guarding)
            {
                idleSet = true;
                int animation = Random.Range(0, 3);
                animator.SetInteger("Animation_int", animation);                
            }
            FreeWayPointsController.wayPointsController.SetFree(nextWP, false);
        }
    }

    void NextGuardPoint(bool wasSeated)
    {
        int nextPoint = 0;
        while (true)
        {
            //Start with 1 for not using the parent transform
            nextPoint = Random.Range(1, patrolPoints.Length);
            print("WP: " + nextPoint);
            nextTarget = patrolPoints[nextPoint];
            nextWP = nextPoint;
            if (nextTarget == currentTarget || FreeWayPointsController.wayPointsController.getFreeState(nextPoint) == true) break;            
        }
        if (nextTarget != currentTarget)
        {
            navMeshAgent.SetDestination(nextTarget.position);
            navMeshAgent.isStopped = false;
            idleSet = false;

        }
        //if(lastWP != nextWP && !wasSeated)
        FreeWayPointsController.wayPointsController.SetFree(lastWP, true);
        currentTarget = patrolPoints[nextPoint];        
        lastWP = nextWP;
        guardingTime = Random.Range(minGuardingTime, maxGuardingTime);
        currentState = State.Patrolling;
    }
}
