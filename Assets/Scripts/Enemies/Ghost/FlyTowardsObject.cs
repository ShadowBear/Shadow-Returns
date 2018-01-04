using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowardsObject : MonoBehaviour {

    public GameObject followObject;
    private Vector3 moveDirection = Vector3.zero;
    public float followSpeed = 5f;

    public float speed = 0;
    Vector3 lastPosition = Vector3.zero;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if(followObject == null) followObject = GameObject.FindGameObjectWithTag("GhostKiller");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = (followObject.transform.position - transform.position);
        transform.position += Vector3.Normalize(new Vector3(moveDirection.x, 0, moveDirection.z)) * Time.deltaTime * followSpeed;
        
        if (anim != null)
        {
            //CheckSpeed for Animation
            speed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("speed", speed);
        }
    }
}
