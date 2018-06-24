using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Standart 8f
    public float speed = 8.0f;
    //Standart 8.5f
    [Range(1,10)]
    public float jumpHeight = 8.5f;
    public float groundDistance = 0.2f;
    public float dashDistance = 5f;
    public LayerMask ground;

    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody playerRigidbody;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private Transform groundCheckTrans;
    private Shield shield;

    private Animator anim;
    //Animation Speed
    private float animSpeed = 0;
    private Vector3 lastPosition = Vector3.zero;
    private PlayerAttack attackScript;

    private Vector3 rotationOffset;

    // Webplayer Beispiel Bedingung

//#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
    // PC
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        shield = GetComponentInChildren<Shield>();
        anim = GetComponentInChildren<Animator>();
        attackScript = GetComponentInChildren<PlayerAttack>();
    }
//#else
//    // Android
//    public GameObject joystickObject;
//    private VirtualJoystick vrStick;

//    void Start()
//    {
//        playerRigidbody = GetComponent<Rigidbody>();
//        vrStick = joystickObject.GetComponent<VirtualJoystick>();
//    }
//#endif


    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheckTrans.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        if (anim != null)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //CheckSpeed for Animation
            animSpeed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
            anim.SetFloat("Speed", animSpeed);
            MoveAnimation(h, v);
        }
    }

    void Update () {
//#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        //Vector3 inputs = Vector3.zero;

        

        RotateWithCamera();

//#else
//        float h = vrStick.Horizontal();
//        float v = vrStick.Vertical(); 
//#endif

        //if (Input.GetButtonDown("Jump")) print("Springe");
        // Ground With Layers for later ToDo
        

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection *= speed * Time.deltaTime;
        //transform.Translate(moveDirection);
        //this.characterController.Move(moveDirection);

        /* ******************** Shield *************************/
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(!attackScript.isAttacking && !attackScript.isReloading) shield.ActivateShield();
        }
        else
        {
            if(shield != null) shield.DeActivateShield();
        }
        /************************************************************/

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<PlayerHealth>().DrinkPotion(GameManager.control.potionValue);
        }

        /* ********************** Dash *************************/

        //if (Input.GetButtonDown("Dash"))
        if(Input.GetButtonDown("Dash"))
        {
            DashImageControll[] dashBlocks = GameManager.control.dashBlocksParent.GetComponentsInChildren<DashImageControll>();
            foreach(DashImageControll dash in dashBlocks)
            {
                if (dash.CanDash())
                {
                    dash.Dashing();
                    Vector3 dashDirection = transform.GetComponentInChildren<PlayerRotation>().transform.forward;
                    Vector3 dashVector = dashDirection * dashDistance;
                    Vector3 targetPosition;
                    Ray dashRay = new Ray(transform.position + playerRigidbody.centerOfMass, dashDirection);
                    RaycastHit rayHit;
                    if (Physics.Raycast(dashRay, out rayHit, dashDistance, ground.value))
                    {
                        targetPosition = rayHit.point;
                    }
                    else
                    {
                        targetPosition = transform.position + dashVector;
                    }

                    transform.SetPositionAndRotation(targetPosition, transform.rotation);
                    break;
                }
            }
            //old Dash
            //Vector3 dashVelocity = Vector3.Scale(transform.GetChild(1).transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * 8 + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * 8 + 1)) / -Time.deltaTime)));
            //playerRigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }

        /* ********************* End *******************/
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);

    }

    private void MoveAnimation(float h, float v)
    {
        int direction = GetComponentInChildren<PlayerRotation>().GetDirection();
        if (Mathf.Abs(h) > Mathf.Abs(v))
        {
            if (h > 0)
            {
                switch (direction)
                {
                    case 0:
                        anim.SetInteger("Direction", 2);
                        break;
                    case -1:
                        anim.SetInteger("Direction", 1);
                        break;
                    case 1:
                        anim.SetInteger("Direction", 0);
                        break;
                    case 2:
                        anim.SetInteger("Direction", -1);
                        break;
                    default:
                        anim.SetInteger("Direction", 0);
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        anim.SetInteger("Direction", 1);
                        break;
                    case -1:
                        anim.SetInteger("Direction", 2);
                        break;
                    case 1:
                        anim.SetInteger("Direction", -1);
                        break;
                    case 2:
                        anim.SetInteger("Direction", 0);
                        break;
                    default:
                        anim.SetInteger("Direction", 0);
                        break;
                }
            }
        }
        else
        {
            if (v > 0)
            {
                switch (direction)
                {
                    case 0:
                        anim.SetInteger("Direction", -1);
                        break;
                    case -1:
                        anim.SetInteger("Direction", 0);
                        break;
                    case 1:
                        anim.SetInteger("Direction", 2);
                        break;
                    case 2:
                        anim.SetInteger("Direction", 1);
                        break;
                    default:
                        anim.SetInteger("Direction", 0);
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        anim.SetInteger("Direction", 0);
                        break;
                    case -1:
                        anim.SetInteger("Direction", -1);
                        break;
                    case 1:
                        anim.SetInteger("Direction", 1);
                        break;
                    case 2:
                        anim.SetInteger("Direction", 2);
                        break;
                    default:
                        anim.SetInteger("Direction", 0);
                        break;
                }
            }
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

        //Jumping Code
        if (Input.GetButtonDown("Jump") && isGrounded) playerRigidbody.velocity = Vector3.up * jumpHeight;
        

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
