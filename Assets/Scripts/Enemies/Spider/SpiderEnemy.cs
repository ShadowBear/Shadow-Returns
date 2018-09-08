using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEnemy : EnemyAIController {
    
    // Use this for initialization
	new void Start () {
        ID = 0;
        base.Start();
        
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    protected override void DoAnimation()
    {
        if (anim != null)
        {
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            if (speed > 0.1f) anim.SetBool("Move Forward Slow", true);
            else anim.SetBool("Move Forward Slow", false);
        }
    }

    protected override void CalculateState()
    {
        if (!gameObject.activeSelf) return;
        if (distanceToPlayer > followDistance) agent.isStopped = true;
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
        isAttacking = true;
        if (anim != null) anim.SetTrigger("Claw Attack");
        hitbox.enabled = true;
        yield return new WaitForSeconds(attackRateTime);
        hitbox.enabled = false;
        isAttacking = false;
        yield return null;
    }

}
