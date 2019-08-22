using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndroidMovement : MonoBehaviour
{

    //Standart 8f
    private float defaultSpeed = 12.0f;
    private float shootingSpeed = 5.5f;
    public float speed = 8.0f;
    //Standart 8.5f
    [Range(1, 10)]
    public float jumpHeight = 8.5f;

    [Range(0, 5)]
    public float jumpTime = 1;
    public Vector3 jumpVector = new Vector3(0, 100, 0);
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
    private Footsteps footstepsScript;

    public GameObject joystickObject;
    private VirtualJoystick vrStick;

    void Start()
    {
        vrStick = joystickObject.GetComponent<VirtualJoystick>();
        playerRigidbody = GetComponent<Rigidbody>();
        shield = GetComponentInChildren<Shield>();
        anim = GetComponentInChildren<Animator>();
        attackScript = GetComponentInChildren<PlayerAttack>();
        footstepsScript = GetComponentInChildren<Footsteps>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheckTrans.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        float h = vrStick.Horizontal();
        float v = vrStick.Vertical();

        Move(h, v);

        if (Input.GetButtonDown("Jump") && isGrounded) StartCoroutine(JumpRoutine());

    }

    void Update()
    {

        if (anim != null && Time.frameCount % 5 == 0)
        {
            float h2 = vrStick.Horizontal();
            float v2 = vrStick.Vertical(); 

            //CheckSpeed for Animation
            animSpeed = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            if (animSpeed > 0.1f && isGrounded) footstepsScript.walking = true;
            else footstepsScript.walking = false;
            lastPosition = transform.position;
            anim.SetFloat("Speed", animSpeed);
            MoveAnimation(h2, v2);
        }

        /* ******************** Shield *************************/
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!attackScript.GetAttackStatus() && !attackScript.GetReloadStatus()) shield.ActivateShield();
        }
        else
        {
            if (shield != null) shield.DeActivateShield();
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
        if (Input.GetButtonDown("Dash"))
        {
            DashImageControll[] dashBlocks = GameManager.control.dashBlocksParent.GetComponentsInChildren<DashImageControll>();
            foreach (DashImageControll dash in dashBlocks)
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
        }
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
            moveDirection += Camera.main.transform.forward * vrStick.Vertical();
            moveDirection += Camera.main.transform.right * vrStick.Horizontal();
            moveDirection.y = 0;
            moveDirection = moveDirection.normalized * speed * Time.deltaTime;

            playerRigidbody.MovePosition(transform.position + moveDirection);
        }
    }

  
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
}
