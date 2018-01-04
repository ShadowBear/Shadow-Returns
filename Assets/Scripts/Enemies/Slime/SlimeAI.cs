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
    private bool isAttacking = false;
    private bool isMoving = false;

    //Animation Speed
    private float speed = 0;
    private Vector3 lastPosition = Vector3.zero;

    public float maxDistanceToPlayer = 5f;
    public float maxDistanceToMeele = 0.5f;
    public float smooth = 2.0f;
    public float attackRateTime = 1.0f;
    public float waitAttackTime = 0.75f;
    public float fireForce = 5f;
    public BoxCollider hitbox;

    public int meeleDMG = 15;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SlimeHealth>().isDead)
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
        if (distanceToPlayer > maxDistanceToMeele)
        {
            //Spieler zu weit entfernt für Fernkampfangriff
            WalkToPlayer();
        }else 
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
        yield return new WaitForSeconds(waitAttackTime);
        if (anim != null) anim.SetTrigger("attack");
        hitbox.enabled = true;
        yield return new WaitForSeconds(attackRateTime);
        hitbox.enabled = false;
        isAttacking = false;
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
