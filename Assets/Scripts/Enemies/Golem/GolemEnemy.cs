﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemy : EnemyAIController {

    [SerializeField]
    private bool patrolGolem = false;
    public GameObject patrolParent;
    [SerializeField]
    private Transform[] patrolPoints;
    private Vector3 destination;
    private int destinationCounter = 0;

    private State currentState;
    private float guardTime = 2.5f;

    enum State { Guard, Chase, Attack, Patrol};

    // Use this for initialization
    new void Start()
    {
        base.Start();
        patrolPoints = patrolParent.GetComponentsInChildren<Transform>();
        if (patrolPoints.Length > 0)
        {
            destinationCounter = ((destinationCounter) % (patrolPoints.Length - 1)) + 1;
            destination = patrolPoints[destinationCounter].position;
            agent.SetDestination(destination);
            currentState = State.Patrol;
        }
        ID = 1;

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }
    
    protected override void CalculateState()
    {
        if (!gameObject.activeSelf) return;
        if (distanceToPlayer > followDistance)
        {
            if (!patrolGolem) agent.isStopped = true;
            else Patroling();
        }
        else if (distanceToPlayer > minDistanceToRange) WalkToPlayer();
        else if (distanceToPlayer > minDistanceToMelee && !meleeOnly)
        {
            //Auf Spieler zulaufen und Angreifen
            //Wechsel Fernkampfangriff zwischen dem Laufen Todo
            if (canshoot && !isAttacking && !isHit) StartCoroutine(RangeAttack());
            else if (!isAttacking) WalkToPlayer();
        }
        else if (distanceToPlayer > minDistanceToMelee) WalkToPlayer();
        else
        {
            //Meele Attack
            if (!isAttacking && !isHit) StartCoroutine(MeeleAttack());
            if (!isCircling && !isHit) StartCoroutine(CiclePlayer());
            else transform.LookAt(player.transform.position);
        }
    }

    void Patroling()
    {
        if (agent.remainingDistance < 1.25f && currentState == State.Patrol)
        {
            currentState = State.Guard;
            agent.isStopped = true;
            guardTime = Random.Range(3f, 7f);
        }
        else if (currentState == State.Guard)
        {
            guardTime -= Time.deltaTime;

            if (guardTime <= 0)
            {
                destinationCounter = ((destinationCounter) % (patrolPoints.Length - 1)) + 1;
                agent.SetDestination(patrolPoints[destinationCounter].position);
                currentState = State.Patrol;
                agent.isStopped = false;
            }
        }
    }


    new protected IEnumerator RangeAttack()
    {
        //print("SpiderRange");
        isAttacking = true;
        agent.isStopped = true;
        canshoot = false;
        if (anim != null) anim.SetTrigger("Cast Spell");
        float animationDelay = 0.5f;
        yield return new WaitForSeconds(animationDelay);
        shotTransform.LookAt(player.transform);
        GameObject shooted = Instantiate(shot, shotTransform.position, shot.transform.rotation);
        shooted.GetComponent<Rigidbody>().velocity = fireForce * shotTransform.forward;
        yield return new WaitForSeconds(attackRateTime - animationDelay);
        isAttacking = false;
        agent.isStopped = false;
        yield return new WaitForSeconds(rangeAttackWaitTime);
        canshoot = true;
        yield return null;
    }

    new protected IEnumerator MeeleAttack()
    {
        print("Punch");
        isAttacking = true;
        if (anim != null) anim.SetTrigger("Punch Attack");
        hitbox.enabled = true;
        yield return new WaitForSeconds(attackRateTime);
        hitbox.enabled = false;
        isAttacking = false;
        yield return null;
    }

    protected override void DoAnimation()
    {
        if (anim != null)
        {
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            if (speed > 0.1f) anim.SetBool("Walk Forward", true);
            else anim.SetBool("Walk Forward", false);
        }
    }
}
