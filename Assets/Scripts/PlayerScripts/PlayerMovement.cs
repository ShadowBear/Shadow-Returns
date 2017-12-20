using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody playerRigidbody;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection *= speed * Time.deltaTime;
        //transform.Translate(moveDirection);
        //this.characterController.Move(moveDirection);
    }

    private void Move(float h, float v)
    {
        moveDirection.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        moveDirection = moveDirection.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + moveDirection);
    }
}
