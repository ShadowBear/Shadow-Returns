using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{

    private NavMeshAgent agent;
    private float distanceToPlayer;
    private GameObject player;
    private Animator anim;
    private Rigidbody rigid;
    private bool isAttacking = false;
    private bool isCircling = false;

    //Animation Speed
    private float speed = 0;
    private Vector3 lastPosition = Vector3.zero;

    //Distance Radii for Attacking and Follow
    // > 25 Stop Follow
    //25...15 WalktoPlayer
    //15...6 Range & WalktoPlayer
    //6...2.5 Walk
    //2.5...0 Melee
    public float followDistance = 25f;
    public float minDistanceToRange = 15f;
    public float walkDistanceToMelee = 6f;
    public float minDistanceToMelee = 2.5f;

    public float smooth = 2.0f;
    public float attackRateTime = 1.0f;
    public float maxWaitAttackTime = 1.25f;
    public float minWaitAttackTime = 0.625f;
    public float dodgeWaitTime = 1.5f;

    //RangeAttackTime 5...12f
    [Header("Range Attack")]
    public float minRangeAttackTime = 5f;
    public float maxRangeAttackTime = 12f;
    private float rangeAttackTime;
    private bool canRangeAttack = false;
    [SerializeField]
    private GameObject rangeAttackTransParent;
    public GameObject rangeAmmu;

    [Header("Hit")]
    public bool isHit;
    public int hitCounter = 0;
    public int maxHitsTaken = 3;
    public float hitDelayTime = 0.5f;

    public float fireForce = 5f;
    public float circleRadius = 2f;
    public float dodgeForce = 50f;

    //Wahrscheinlichkeit mit der ausgewichen werden soll 0.5 = 50%; 0.8 = 80%;
    //Darf nicht größer 1 sein!! 
    public float dodgeChance = 0.5f;
    public BoxCollider hitbox;

    public int meeleDMG = 15;

    // Use this for initialization
    void Start()
    {
        isHit = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        hitbox.enabled = false;
        rangeAttackTime = minRangeAttackTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<SlimeHealthChild>().isDead)
        {
            agent.isStopped = true;
            return;
        }

        distanceToPlayer = Mathf.Abs((transform.position - player.transform.position).magnitude);
        DoAnimation();
        CalculateAttackState();


    }

    public void TakeHit()
    {
        if (!isHit)
        {
            if (hitCounter <= maxHitsTaken)
            {
                isHit = true;
                hitCounter++;
                StartCoroutine(HitReset());
            }
            else
            {
                isHit = true;
                StartCoroutine(CounterAttack());
                hitCounter = 0;
            }
        }
        
    }

    void WalkToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void DoAnimation()
    {
        if (anim != null)
        {
            //CheckSpeed for Animation
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("speed", speed);
        }
    }

    void CalculateAttackState()
    {
        if (distanceToPlayer > followDistance) agent.isStopped = true;
        //Spieler zu weit entfernt für Fernkampfangriff
        else if (distanceToPlayer > minDistanceToRange) WalkToPlayer();
        else if (distanceToPlayer > walkDistanceToMelee)
        {
            if (canRangeAttack && !isHit)
            {
                canRangeAttack = false;
                StartCoroutine(RangeAttack());
            }
            else
            {
                WalkToPlayer();
                
                if (rangeAttackTime <= 0 && !canRangeAttack)
                {
                    canRangeAttack = true;
                    rangeAttackTime = Random.Range(minRangeAttackTime,maxRangeAttackTime);
                }else rangeAttackTime -= Time.deltaTime;
            }
        }
        else if (distanceToPlayer > minDistanceToMelee) WalkToPlayer();
        else
        {
            //Meele Attack
            if (!isAttacking && !isHit) StartCoroutine(MeeleAttack());

            //Circle Player
            if (!isCircling) StartCoroutine(CirclePlayer());
            else transform.LookAt(player.transform.position);

            //Ausweichen falls Angriff kommt und ausweichen darf 
            //Todo for Better Results
            //if (player.GetComponentInChildren<PlayerAttack>().GetAttackStatus() && !hasDodged && rigid != null)
            //{
            //    //Wahrscheinlichkeit mit der ausgewichen werden soll 0.5 = 50%; 0.8 = 80%;
            //    if (Random.Range(0, 1) < dodgeChance) StartCoroutine(DodgeAttack());
            //    print("Dodge");
            //}
        }
    }

    IEnumerator CirclePlayer()
    {
        isCircling = true;
        agent.SetDestination(player.transform.position + ((Vector3)Random.insideUnitCircle * circleRadius));
        float waitAttackTime = Random.Range(minWaitAttackTime, maxWaitAttackTime);
        yield return new WaitForSeconds(waitAttackTime);
        isCircling = false;
        yield return null;
    }

    IEnumerator DodgeAttack()
    {
        int dodgeDirection = Random.Range(0, 3);
        switch (dodgeDirection)
        {
            case 0: rigid.AddForce(-transform.forward.normalized * dodgeForce, ForceMode.Impulse);
                break;
            case 1:
                rigid.AddForce(-transform.right.normalized * dodgeForce, ForceMode.Impulse);
                break;
            case 2:
                rigid.AddForce(transform.right.normalized * dodgeForce, ForceMode.Impulse);
                break;
        }
        rigid.AddForce(-transform.forward.normalized * dodgeForce, ForceMode.Impulse);
        yield return new WaitForSeconds(dodgeWaitTime);
        yield return null;
    }

    IEnumerator MeeleAttack()
    {
        isAttacking = true;
        float waitAttackTime = Random.Range(minWaitAttackTime, maxWaitAttackTime);
        yield return new WaitForSeconds(waitAttackTime);
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
        float waitAttackTime = Random.Range(minWaitAttackTime, maxWaitAttackTime);
        yield return new WaitForSeconds(waitAttackTime);
        if (anim != null) anim.SetTrigger("attack");
        yield return new WaitForSeconds(waitAttackTime/2);
        foreach (Transform child in rangeAttackTransParent.GetComponentsInChildren<Transform>())
        {
            if (rangeAttackTransParent.transform == child) continue;
            GameObject shot = Instantiate(rangeAmmu, child.transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody>().velocity = fireForce * child.transform.forward;
        }
        
        agent.isStopped = false;
        isAttacking = false;
        yield return null;
    }

    IEnumerator CounterAttack()
    {
        if (Random.Range(1, 3) == 1) StartCoroutine(RangeAttack());
        else StartCoroutine(MeeleAttack());
        yield return new WaitForSeconds(hitDelayTime);
        isHit = false;
        yield return null;
    }

    IEnumerator HitReset()
    {
        yield return new WaitForSeconds(hitDelayTime);
        isHit = false;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(meeleDMG);
        }
    }
}
