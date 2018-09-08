using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaEnemy : EnemyAIController {

    // Use this for initialization
    new void Start()
    {
        ID = 1;
        base.Start();
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
        print("Punch");
        isAttacking = true;
        if (anim != null)
        {
            if(Random.Range(0,2) == 0) anim.SetTrigger("Attack 01");
            else anim.SetTrigger("Attack 02");
        }
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
            if (speed > 0.1f) anim.SetBool("Move", true);
            else anim.SetBool("Move", false);
        }
    }
}
