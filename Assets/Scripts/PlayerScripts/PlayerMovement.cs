using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Standart 8f
    private float defaultSpeed = 12.0f;
    private float shootingSpeed = 5.5f;
    public float speed = 8.0f;
    //Standart 8.5f
    [Range(1,10)]
    public float jumpHeight = 8.5f;

    [Range(0,5)]
    public float jumpTime = 1;
    public Vector3 jumpVector = new Vector3(0,100,0);
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

    //private CharacterController charController;
    //private static float GRAVITY = Physics.gravity.y;
    //private Vector3 velocity = Vector3.zero;

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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //print("Springe wirklich");
            //playerRigidbody.velocity = Vector3.up * jumpHeight;

            /* NEW */
            StartCoroutine(JumpRoutine());
        }
        //if (anim != null)
        //{
        //    float h = Input.GetAxisRaw("Horizontal");
        //    float v = Input.GetAxisRaw("Vertical");
        //    //CheckSpeed for Animation
        //    animSpeed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
        //    //animSpeed = playerRigidbody.velocity.magnitude;
        //    lastPosition = transform.position;
        //    anim.SetFloat("Speed", animSpeed);
        //    MoveAnimation(h, v);
        //}
    }

    void Update () {
        //#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        //Vector3 inputs = Vector3.zero;
        
        if (anim != null && Time.frameCount % 5 == 0)
        {
            float h2 = Input.GetAxisRaw("Horizontal");
            float v2 = Input.GetAxisRaw("Vertical");
            //CheckSpeed for Animation
            animSpeed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            //animSpeed = playerRigidbody.velocity.magnitude;
            lastPosition = transform.position;
            anim.SetFloat("Speed", animSpeed);
            MoveAnimation(h2, v2);
        }

        //RotateWithCamera();

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
            if(!attackScript.GetAttackStatus() && !attackScript.GetReloadStatus()) shield.ActivateShield();
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GetComponentInChildren<Weapon>()) GetComponentInChildren<Weapon>().Reload();
            
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
        

    }

    IEnumerator JumpRoutine()
    {
        playerRigidbody.velocity = Vector3.zero;
        float timer = 0;

        while (Input.GetButton("Jump") && timer < jumpTime)
        {
            //Calculate how far through the jump we are as a percentage
            //apply the full jump force on the first frame, then apply less force
            //each consecutive frame

            float proportionCompleted = timer / jumpTime;
            Vector2 thisFrameJumpVector = Vector3.Lerp(jumpVector, Vector3.zero, proportionCompleted);
            playerRigidbody.AddForce(thisFrameJumpVector);
            timer += Time.deltaTime;
            yield return null;
        }
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

    /* Altes Movement ohne CharControll */

    private void Move(float h, float v)
    {
        if ((attackScript.GetAttackStatus() && !attackScript.rangeAttack) || attackScript.GetReloadStatus())
        {
            return;
        }

        /** Had 2 Be Commented for Movement towards Camera Rotation **///
        if (h != 0 || v != 0)
        {
            moveDirection = Vector3.zero;
            moveDirection += Camera.main.transform.forward * Input.GetAxis("Vertical");
            moveDirection += Camera.main.transform.right * Input.GetAxis("Horizontal");
            moveDirection.y = 0;
            moveDirection = moveDirection.normalized * speed * Time.deltaTime;
           
            playerRigidbody.MovePosition(transform.position + moveDirection);
        }

        //Jumping Code
        


    }

    /********** CharController Test *******************/
    //private void Move(float h, float v)
    //{
    //    if ((attackScript.GetAttackStatus() && !attackScript.rangeAttack) || attackScript.GetReloadStatus())
    //    {
    //        return;
    //    }
    //    //moveDirection = new Vector3(h, 0, v); 
    //    // Normalise the movement vector and make it proportional to the speed per second.


    //    // Move the player to it's current position plus the movement.

    //    /** Had 2 Be Commented for Movement towards Camera Rotation **///
    //    if (h != 0 || v != 0)
    //    {
    //        if (charController.isGrounded)
    //        {
    //            // We are grounded, so recalculate
    //            // move direction directly from axes
    //            moveDirection = Vector3.zero;
    //            moveDirection += Camera.main.transform.forward * Input.GetAxis("Vertical");
    //            moveDirection += Camera.main.transform.right * Input.GetAxis("Horizontal");
    //            moveDirection = moveDirection * speed;
    //        }

    //        moveDirection = moveDirection.normalized * speed * Time.deltaTime;
    //    }
    //    else moveDirection.Set(0, moveDirection.y, 0);

    //    // Apply gravity
    //    moveDirection.y += (GRAVITY * Time.deltaTime);
    //    //if (isGrounded && charController.velocity.y < 0)
    //    //    velocity.y = 0f;
    //    //moveDirection += velocity;
    //    //charController.Move(velocity * Time.deltaTime);
    //    // Move the controller
    //    charController.Move(moveDirection * Time.deltaTime);

    //    //Jumping Code
    //    //if (Input.GetButtonDown("Jump") && isGrounded) playerRigidbody.velocity = Vector3.up * jumpHeight;


    //}
    /*************************************************************************/

    //private void RotateWithCamera()
    //{
    //    //rotationOffset = Camera.main.transform.position;
    //    //rotationOffset.y = transform.position.y;
    //    //Vector3.RotateTowards(transform.position, rotationOffset, (speed * Time.deltaTime), 0.0f);
    //    //Debug.Log("Rotate");

    //    //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    //moveDirection += Camera.main.transform.forward * Input.GetAxis("Vertical");
    //    //moveDirection += Camera.main.transform.right * Input.GetAxis("Horizontal");
    //    //transform.Translate(-moveDirection * Time.deltaTime * speed);
    //}

    /// <summary>
    /// Return different movementspeed
    /// 1 = Normal
    /// 2 = Shooting
    /// </summary>
    /// <returns>
    /// 1:NormalSpeed
    /// 2: ShootingSpeed</returns>
    public float GetSpeed(int i)
    {
        if (i == 1) return defaultSpeed;
        else if (i == 2) return shootingSpeed;
        return defaultSpeed;
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (!collision.collider.isTrigger) playerRigidbody.velocity.Set(0,0,0);
    //}

}
