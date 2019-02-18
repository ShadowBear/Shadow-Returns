using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBossController : MonoBehaviour {

    // ********* Attack Rate und SwapTime auf 0.5f Witziges Feature evtl. als Schwierigkeitsgrad nutzbar *****// 


    GameObject player;
    NavMeshAgent navAgent;
    public LayerMask layer;
    float testCounter = 0f;
    private float distanceToPlayer;
    public float minDistanceBeforeAttack = 5;
    public int minDistanceBeforeDodge = 8;
    public float maxShootingDistance = 10;
    public float swapTime = 2f;
    public float waitDodgeTime = 1f;

    public bool playerInView = true;
    public bool rageMode = false;
    public ParticleSystem rageParticle;
    public Transform shotTransform;

    private GhostBossAttack attackScript;
    

    
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        attackScript = GetComponent<GhostBossAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (distanceToPlayer < minDistanceBeforeAttack)
        {
            transform.LookAt(player.transform);
            RaycastHit hit;
            if (Physics.Raycast(shotTransform.position, shotTransform.forward, out hit, maxShootingDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (testCounter > swapTime)
                    {
                        testCounter = 0;
                        if(rageMode) RoundThePlayer();
                        attackScript.AttackEnemy(distanceToPlayer);
                    }
                }
                else
                {
                    navAgent.isStopped = false;
                    navAgent.SetDestination(player.transform.position);
                }
            }                       
            testCounter += Time.deltaTime;
        }
        else
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(player.transform.position);
        }
        
        
	}

    IEnumerator Dodge(bool sidewards)
    {
        NavMeshHit hit;
        if (sidewards)
        {
            if (Random.Range(0, 2) == 1)
            {
                if (NavMesh.SamplePosition(transform.position + 2 * Vector3.left, out hit, 0.5f, layer))
                    navAgent.Warp(hit.position);
                else if (NavMesh.SamplePosition(transform.position + 2 * Vector3.right, out hit, 0.5f, layer))
                    navAgent.Warp(hit.position);
            }
            else
            {
                if (NavMesh.SamplePosition(transform.position + 2 * Vector3.right, out hit, 0.5f, layer))
                    navAgent.Warp(hit.position);
                else if (NavMesh.SamplePosition(transform.position + 2 * Vector3.left, out hit, 0.5f, layer))
                    navAgent.Warp(hit.position);
            }                
        }
        else
        {
            if (NavMesh.SamplePosition(transform.position + 2 * Vector3.back, out hit, 0.5f, layer))
                navAgent.Warp(hit.position);
        }
        yield return new WaitForSeconds(waitDodgeTime);
        yield return null;

    }

    void RoundThePlayer()
    {
        // get a random direction (360°) in radians
        float angle = Random.Range(0.0f, Mathf.PI * 2);

        // create a vector with length 1.0
        Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        // scale it to the desired length

        NavMeshHit hit;
        //Check if Position is on Navmesh 
        if (NavMesh.SamplePosition(transform.position + V * minDistanceBeforeAttack, out hit, 1, layer))
            navAgent.Warp(player.transform.position + V * minDistanceBeforeAttack);

        transform.LookAt(player.transform.position);
    }

    public void SetRageMode(bool state, float leftHealthPercentage)
    {
        rageMode = true;
        navAgent.speed *= 1.5f;
        swapTime = 0.5f + 0.5f * (1 - leftHealthPercentage);
        GetComponent<GhostBossAttack>().attackRateTime *= 0.5f;
        rageParticle.Play();
    }
}
