using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {

    private Rigidbody rigid;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    // Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
    /* ****** Old One
	// Update is called once per frame
	void FixedUpdate () {
		if(rigid.velocity.y < 0)
        {
            rigid.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }else if(rigid.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigid.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
	}
    */

    void FixedUpdate()
    {
        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rigid.velocity.y > 0)
        {
            rigid.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

}
