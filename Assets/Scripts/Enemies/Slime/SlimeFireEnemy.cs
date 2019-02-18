using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFireEnemy : EnemyAIController {

    [SerializeField]
    private bool patrolSlime = false;
    public GameObject patrolParent;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        ID = 7;
        meeleDMG = (int)(enemy.Schleim[0] + enemy.Schleim[2] * (0.5 * enemy.Schleim[0]) - (0.5 * enemy.Schleim[0]));
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
            if (!patrolSlime) agent.isStopped = true;
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
        //Todo
        agent.isStopped = true;
    }


    new protected IEnumerator RangeAttack()
    {
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
        if (anim != null) anim.SetTrigger("Attack");
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
            if (speed > 0.1f) anim.SetFloat("Speed", speed);
            else anim.SetFloat("Speed", speed);
        }
    }
}
