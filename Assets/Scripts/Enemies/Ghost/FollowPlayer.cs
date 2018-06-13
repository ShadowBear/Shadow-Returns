using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private GameObject player;
    private Vector3 moveDirection = Vector3.zero;
    public float smooth = 2.5f;
    public float followSpeed = 0.5f;

    public float speed = 0;
    Vector3 lastPosition = Vector3.zero;
    private Animator anim;

    public float maxDistanceToPlayer = 5f;
    private float distanceToPlayer = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        followSpeed = Random.Range(0.3f, 0.7f);
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        if (GetComponent<HealthScript>() != null) {
            if(GetComponent<HealthScript>().isDead) return;
        }
        distanceToPlayer = Mathf.Abs((player.transform.position - transform.position).magnitude);

        if(distanceToPlayer < maxDistanceToPlayer)
        {
            moveDirection = (player.transform.position - transform.position);
            if (Mathf.Abs(moveDirection.magnitude) > 0.5f) transform.position += new Vector3(moveDirection.x, 0, moveDirection.z) * Time.deltaTime * followSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((player.transform.position - transform.position)), Time.deltaTime * smooth);

        }

        if (anim != null) { 
            //CheckSpeed for Animation
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("speed", speed);
        }
    }
}
