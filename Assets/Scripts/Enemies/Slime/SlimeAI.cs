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
    private bool isMoving = false;
    private bool isCircling = false;
    private bool hasDodged = false;

    //Animation Speed
    private float speed = 0;
    private Vector3 lastPosition = Vector3.zero;

    public float maxDistanceToPlayer = 5f;
    public float maxDistanceToMeele = 0.5f;
    public float smooth = 2.0f;
    public float attackRateTime = 1.0f;
    public float maxWaitAttackTime = 1.25f;
    public float minWaitAttackTime = 0.625f;
    public float dodgeWaitTime = 1.5f;

    [Header("Range Attack")]
    public float startRangeAttackTime = 5f;
    private float rangeAttackTime;
    private bool canRangeAttack = false;
    [SerializeField]
    private GameObject rangeAttackTransParent;
    public GameObject rangeAmmu;


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
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        hitbox.enabled = false;
        rangeAttackTime = startRangeAttackTime;
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
        if (anim != null)
        {
            //CheckSpeed for Animation
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("speed", speed);
        }

        //Spieler zu weit entfernt für Fernkampfangriff
        if (distanceToPlayer > maxDistanceToPlayer)
        {            
            WalkToPlayer();
        }else if(distanceToPlayer < maxDistanceToPlayer && distanceToPlayer > maxDistanceToMeele)
        {
            if (canRangeAttack)
            {
                canRangeAttack = false;
                StartCoroutine(RangeAttack());
            }
            else
            {
                WalkToPlayer();
                rangeAttackTime -= Time.deltaTime;
                if(rangeAttackTime <= 0)
                {
                    canRangeAttack = true;
                    rangeAttackTime = startRangeAttackTime;
                }
            }
            
        }else
        {
            //Meele Attack
            if (!isAttacking) StartCoroutine(MeeleAttack());

            //Circle Player
            if (!isCircling) StartCoroutine(CirclePlayer());
            if (isCircling) transform.LookAt(player.transform.position);

            //Ausweichen falls Angriff kommt und ausweichen darf
            if (player.GetComponentInChildren<PlayerAttack>().GetAttackStatus() && !hasDodged && rigid != null)
            {
                //Wahrscheinlichkeit mit der ausgewichen werden soll 0.5 = 50%; 0.8 = 80%;
                if(Random.Range(0,1) < dodgeChance)StartCoroutine(DodgeAttack());
            }
        }
    }

    void WalkToPlayer()
    {
        agent.SetDestination(player.transform.position);
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
        hasDodged = true;
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
        hasDodged = false;
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
        print("RangeAttack");
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

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //col.GetComponent<HealthScript>().TakeDamage(meeleDMG);
            col.GetComponent<PlayerHealth>().TakeDamage(meeleDMG);
        }
    }
}
