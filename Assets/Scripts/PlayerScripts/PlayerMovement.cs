using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6.0f;
    [Range(1,10)]
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public float dashDistance = 5f;
    public LayerMask ground;

    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody playerRigidbody;
    [SerializeField]
    private bool isGrounded = true;
    private Transform groundCheckTrans;
    private Shield shield;

    private Animator anim;
    //Animation Speed
    private float animSpeed = 0;
    private Vector3 lastPosition = Vector3.zero;
    private PlayerAttack attackScript;

    private Vector3 rotationOffset;

    // Webplayer Beispiel Bedingung

#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
    // PC
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        shield = GetComponentInChildren<Shield>();
        groundCheckTrans = transform.GetChild(0);
        anim = GetComponentInChildren<Animator>();
        attackScript = GetComponentInChildren<PlayerAttack>();
    }
#else
    // Android
    public GameObject joystickObject;
    private VirtualJoystick vrStick;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        vrStick = joystickObject.GetComponent<VirtualJoystick>();
    }
#endif


    // Update is called once per frame
    void FixedUpdate () {
#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        //Vector3 inputs = Vector3.zero;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        RotateWithCamera();

#else
        float h = vrStick.Horizontal();
        float v = vrStick.Vertical(); 
#endif
        // Ground With Layers for later ToDo
        isGrounded = Physics.CheckSphere(groundCheckTrans.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection *= speed * Time.deltaTime;
        //transform.Translate(moveDirection);
        //this.characterController.Move(moveDirection);

        /* ******************** Shield *************************/
        if (Input.GetKey(KeyCode.LeftShift))
        {

            if(!attackScript.isAttacking && !attackScript.isReloading) shield.activateShield();
        }
        else
        {
            if(shield != null) shield.deActivateShield();
        }
        /************************************************************/

        /* **********************Jumping & Dash *************************/

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
             playerRigidbody.velocity = Vector3.up * jumpHeight;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.GetChild(1).transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * 8 + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * 8 + 1)) / -Time.deltaTime)));
            playerRigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }

        /* ********************* End *******************/
        Move(h,v);
        if (anim != null)
        {
            //CheckSpeed for Animation
            animSpeed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("Speed", animSpeed);
        }
    }

    private void Move(float h, float v)
    {
        //moveDirection = new Vector3(h, 0, v); 
        // Normalise the movement vector and make it proportional to the speed per second.


        // Move the player to it's current position plus the movement.

        /** Had 2 Be Commented for Movement towards Camera Rotation **///
        if (h != 0 || v != 0)
        {
            moveDirection = Vector3.zero;
            moveDirection += Camera.main.transform.forward * Input.GetAxis("Vertical");
            moveDirection += Camera.main.transform.right * Input.GetAxis("Horizontal");

            moveDirection = moveDirection.normalized * speed * Time.deltaTime;

            playerRigidbody.MovePosition(new Vector3(transform.position.x + moveDirection.x, transform.position.y, transform.position.z + moveDirection.z));
        }
        
    }

    private void RotateWithCamera()
    {
        //rotationOffset = Camera.main.transform.position;
        //rotationOffset.y = transform.position.y;
        //Vector3.RotateTowards(transform.position, rotationOffset, (speed * Time.deltaTime), 0.0f);
        //Debug.Log("Rotate");

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection += Camera.main.transform.forward * Input.GetAxis("Vertical");
        //moveDirection += Camera.main.transform.right * Input.GetAxis("Horizontal");
        //transform.Translate(-moveDirection * Time.deltaTime * speed);
    }
}
