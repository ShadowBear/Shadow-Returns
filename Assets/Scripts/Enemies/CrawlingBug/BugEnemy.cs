using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEnemy : EnemyAIController {

    new void Start()
    {
        base.Start();
        ID = 2;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void CalculateState()
    {
        if (!gameObject.activeSelf) return;
        if (distanceToPlayer > followDistance) agent.isStopped = true;
        else if (distanceToPlayer > minDistanceToMelee) WalkToPlayer();
        else
        {
            //Meele Attack
            if (!isAttacking && !isHit) StartCoroutine(MeeleAttack());
            if (!isCircling && !isHit) StartCoroutine(CiclePlayer());
            else transform.LookAt(player.transform.position);
        }
    }

    new protected IEnumerator MeeleAttack()
    {
        isAttacking = true;
        if (anim != null) anim.SetTrigger("Attack 01");
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
            if (speed > 0.1f) anim.SetBool("Walk", true);
            else anim.SetBool("Walk", false);
        }
    }

}
