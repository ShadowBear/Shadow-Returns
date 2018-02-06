using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject shot;
    public GameObject light;
    public Transform fireTransform;
    public float fireForce = 5;

    public float fireRate = 0.5f;
    public bool isAttacking = false;
    public bool fireStickDown = false;
    private bool isShielded = false;
    public HealthScript healthScript;

    //Range AttackBoolen & Collider for Meele
    public bool rangeAttack = true;
    public Collider meeleHitbox;

    private Animator anim;
    public ParticleSystem fireParticle;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        meeleHitbox.enabled = false;        
	}
	
	// Update is called once per frame
	void Update () {
        CheckFired();
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SwapWeapon();
        }
	}

    private void CheckFired()
    {
        
#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            isShielded = healthScript.isShielded;
            if (!isAttacking && !isShielded)
            {
                if (rangeAttack) StartCoroutine(Fire());
                else StartCoroutine(MeeleHit());
            }            
        }
        if (Input.GetButtonDown("Fire2"))
        {
            isShielded = healthScript.isShielded;
            if (!isAttacking && !isShielded)
            {
                StartCoroutine(Light());
            }            
        }
#else
        if (fireStickDown && rangeAttack && !isShooting) StartCoroutine(Fire());
        else if(fireStickDown && !rangeAttack && !isShooting) StartCoroutine(MeeleHit());
#endif
    }

    IEnumerator Fire()
    {
        isAttacking = true;
        anim.SetBool("Range",true);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.25f);
        GameObject shotInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation);
        shotInstance.GetComponent<Rigidbody>().velocity = fireForce * fireTransform.forward;
        //shotInstance.GetComponent<ParticleSystem>().Play();
        //fireParticle.Play();
        yield return new WaitForSeconds(fireRate - 0.25f);
        isAttacking = false;
        yield return null;
        
    }

    IEnumerator MeeleHit()
    {
        isAttacking = true;
        meeleHitbox.enabled = true;
        anim.SetBool("Range", false);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(fireRate);
        isAttacking = false;
        meeleHitbox.enabled = false;
        yield return null;

    }

    IEnumerator Light()
    {
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.25f);
        GameObject shotInstance = Instantiate(light, fireTransform.position, fireTransform.rotation);
        yield return null;

    }

    void SwapWeapon()
    {
        rangeAttack = rangeAttack ? false : true;
    }

    public bool getAttackStatus()
    {
        return isAttacking;
    }
}
