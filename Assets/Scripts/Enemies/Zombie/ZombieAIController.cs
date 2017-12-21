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

    public float rangeAttackWaitTime = 2f;

    //Animation Speed
    private float speed = 0;
    private Vector3 lastPosition = Vector3.zero;

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
        if (anim != null)
        {
            //CheckSpeed for Animation
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("speed", speed);
        }
        if (distanceToPlayer > maxDistanceToPlayer)
        {
            //Spieler zu weit entfernt für Fernkampfangriff
            WalkToPlayer();
        }
        else if (distanceToPlayer <= maxDistanceToPlayer && distanceToPlayer >= maxDistanceToMeele)
        {
            //Auf Spieler zulaufen und Angreifen
            //Wechsel Fernkampfangriff zwischen dem Laufen Todo
            if (canshoot && !isAttacking) StartCoroutine(RangeAttack());
            else if (!isAttacking) WalkToPlayer();
        }
        else
        {
            //Meele Attack
            if (!isAttacking) StartCoroutine(MeeleAttack());
        }
    }

    void WalkToPlayer()
    {
        agent.SetDestination(player.transform.position);
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
        GameObject shooted =  Instantiate(shot, shotTransform.position, shot.transform.rotation);
        shooted.GetComponent<Rigidbody>().velocity = fireForce * shotTransform.forward;
        yield return new WaitForSeconds(attackRateTime);
        isAttacking = false;
        agent.isStopped = false;
        yield return new WaitForSeconds(rangeAttackWaitTime);
        canshoot = true;
        yield return null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<HealthScript>().TakeDamage(meeleDMG);
        }
    }
}
