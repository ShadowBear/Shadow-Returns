using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBossAttack : MonoBehaviour {

    // Use this for initialization

    private Animator anim;
    private bool isAttacking = false;
    private NavMeshAgent agent;
    [SerializeField]
    private float fireForce = 5f;

    public GameObject raTransParent;
    public LayerMask colliderLayer;

    public BoxCollider hitbox;
    public float attackRateTime = 2f;
    public float rangeAttackWaitTime = 2.5f;
    public float chargeForce = 25f;
    public int meeleDMG = 50;
    public bool canShoot = true;
    public Transform shotTransform;
    public GameObject shot;

	void Start () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hitbox.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = shotTransform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(shotTransform.position, forward, Color.green);
    }

    public void AttackEnemy()
    {
        
        if (canShoot && !isAttacking) StartCoroutine(Attack02());       
    }

    public void AttackEnemy(float distanceToPlayer)
    {
        if(distanceToPlayer < 2)
        {
            if (canShoot && !isAttacking) StartCoroutine(MeleeAttack01());
        }else if(distanceToPlayer < 3)
        {
            if (canShoot && !isAttacking) StartCoroutine(Attack03(distanceToPlayer));
        }else if(distanceToPlayer < 6)
        {
            if (canShoot && !isAttacking) StartCoroutine(RangeAttack());
        }
        else
        {
            if (canShoot && !isAttacking) StartCoroutine(Attack02());
        }
       
    }

    IEnumerator MeleeAttack01()
    {
        //print("Attack 01");
        isAttacking = true;
        if (anim != null) anim.SetTrigger("attack");
        hitbox.enabled = true;
        yield return new WaitForSeconds(attackRateTime);
        hitbox.enabled = false;
        isAttacking = false;
        yield return null;
    }

    IEnumerator RangeAttack()
    {
        isAttacking = true;
        agent.isStopped = true;
        canShoot = false;
        if (anim != null) anim.SetTrigger("attack");
        GameObject shooted = Instantiate(shot, shotTransform.position, shot.transform.rotation);
        shooted.GetComponent<Rigidbody>().velocity = fireForce * shotTransform.forward;
        yield return new WaitForSeconds(attackRateTime);
        isAttacking = false;
        agent.isStopped = false;
        //yield return new WaitForSeconds(rangeAttackWaitTime);
        canShoot = true;
        yield return null;
    }

    IEnumerator Attack02()
    {
        isAttacking = true;
        agent.isStopped = true;
        canShoot = false;
        if (anim != null) anim.SetTrigger("attack");
        foreach(Transform trans in raTransParent.GetComponentsInChildren<Transform>())
        {
            GameObject shooted = Instantiate(shot, trans.position, shot.transform.rotation);
            shooted.GetComponent<Rigidbody>().velocity = fireForce * trans.forward;
        }
        yield return new WaitForSeconds(attackRateTime);
        isAttacking = false;
        agent.isStopped = false;
        canShoot = true;
        yield return null;
    }
    // Speed up Attack on Player (Nearly Charge)
    IEnumerator Attack03(float distance)
    {
        //print("Attack 03");
        //float chargeLength = 0;
        //while (chargeLength < 0.7f)
        //{
        //    transform.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * distance * chargeForce, ForceMode.VelocityChange);
        //    chargeLength += Time.deltaTime;
        //}

        Vector3 chargePosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        agent.SetDestination(chargePosition);
        float speed = agent.speed;
        agent.isStopped = false;
        agent.speed = speed * 25;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        agent.speed = speed;
        StartCoroutine(MeleeAttack01());
        yield return null;
    }

    void Attack04()
    {
        //print("Attack 04");
    }
}
