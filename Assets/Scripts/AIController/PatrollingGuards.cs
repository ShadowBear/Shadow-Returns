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


    private NavMeshAgent navMeshAgent;
    private Transform nextTarget;
    private Transform currentTarget;
    private State currentState;
    private Animator animator;
    private int idleAnimation;
    private bool idleSet = false;

    enum State
    {
        Guarding, Patrolling
    };

    // Use this for initialization
    void Start()
    {
        patrolPoints = patrolPointParent.GetComponentsInChildren<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = State.Guarding;
        animator = GetComponent<Animator>();
        animator.SetBool("Static_b", false);
    }


	// Update is called once per frame
	void Update () {

        if (currentState == State.Guarding) {
            if (guardingTime <= 0) nextGuardPoint();
            else
            {
                guardingTime -= Time.deltaTime;
                transform.localEulerAngles = new Vector3(nextTarget.transform.eulerAngles.x, nextTarget.transform.eulerAngles.y, nextTarget.transform.eulerAngles.z);
            }      
        }
        else if(navMeshAgent.remainingDistance < 0.5f)
        {
            currentState = State.Guarding;
            navMeshAgent.isStopped = true;
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
        }
    }

    void nextGuardPoint()
    {
        int nextPoint = Random.Range(1, patrolPoints.Length);
        print("WP: " + nextPoint);
        nextTarget = patrolPoints[nextPoint];
        navMeshAgent.SetDestination(nextTarget.position);
        navMeshAgent.isStopped = false;
        idleSet = false;

        guardingTime = Random.Range(minGuardingTime, maxGuardingTime);
        currentState = State.Patrolling;
    }
}
