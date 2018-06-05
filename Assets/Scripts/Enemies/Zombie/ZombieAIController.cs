using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAIController : MonoBehaviour {

    private NavMeshAgent agent;
    private float distanceToPlayer;
    private GameObject player;
    private Animator anim;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool canshoot = true;
    private bool isCircling = false;

    public float rangeAttackWaitTime = 2f;

    [Header("OnlyMeleeAttack")]
    [SerializeField]
    private bool meleeOnly = false;

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



    public float maxDistanceToPlayer = 5f;
    public float maxDistanceToMeele = 0.5f;


    public float smooth = 2.0f;
    public float attackRateTime = 1.0f;
    public float fireForce = 5f;
    public BoxCollider hitbox;

    public Transform shotTransform;
    public GameObject shot;
    public int meeleDMG = 15;
    //public int rangeDMG = 10;

    public float maxWaitAttackTime = 1.25f;
    public float minWaitAttackTime = 0.625f;

    public float circleRadius = 2f;

    [Header("Hit")]
    public bool isHit;
    public int hitCounter = 0;
    public int maxHitsTaken = 3;
    public float hitDelayTime = 0.5f;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        hitbox.enabled = false;
        //Patrol();
    }

    // Update is called once per frame
    void Update() {
        if (GetComponent<HealthScript>().isDead)
        {
            agent.isStopped = true;
            return;
        }
        distanceToPlayer = Mathf.Abs((transform.position - player.transform.position).magnitude);
        DoAnimation();
        CalculateState();
        
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

    void CalculateState()
    {
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

    void WalkToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public void TakeHit()
    {
        //print("I am Hit");
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

    IEnumerator CiclePlayer()
    {
        isCircling = true;
        agent.SetDestination(player.transform.position + ((Vector3)Random.insideUnitCircle * circleRadius));
        float waitAttackTime = Random.Range(minWaitAttackTime, maxWaitAttackTime);
        yield return new WaitForSeconds(waitAttackTime);
        isCircling = false;
        yield return null;
    }

    IEnumerator MeeleAttack()
    {
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
        canshoot = false;
        if (anim != null) anim.SetTrigger("attack");
        float animationDelay = 0.5f;
        yield return new WaitForSeconds(animationDelay);
        shotTransform.LookAt(player.transform);
        GameObject shooted =  Instantiate(shot, shotTransform.position, shot.transform.rotation);
        shooted.GetComponent<Rigidbody>().velocity = fireForce * shotTransform.forward;
        yield return new WaitForSeconds(attackRateTime- animationDelay);
        isAttacking = false;
        agent.isStopped = false;
        yield return new WaitForSeconds(rangeAttackWaitTime);
        canshoot = true;
        yield return null;
    }

    IEnumerator CounterAttack()
    {
        //print("I Will strike Back");
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
