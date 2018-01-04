using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject shot;
    public GameObject light;
    public Transform fireTransform;
    public float fireForce = 5;

    public float fireRate = 0.5f;
    public bool isShooting = false;
    public bool fireStickDown = false;

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
	}

    private void CheckFired()
    {
#if Unity_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetButtonDown("Fire1") && !isShooting) StartCoroutine(Fire());
        if (Input.GetButtonDown("Fire2")) StartCoroutine(Light());
#else
        if (fireStickDown && rangeAttack && !isShooting) StartCoroutine(Fire());
        else if(fireStickDown && !rangeAttack && !isShooting) StartCoroutine(MeeleHit());
#endif
    }

    IEnumerator Fire()
    {
        isShooting = true;
        anim.SetBool("Range",true);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.25f);
        GameObject shotInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation);
        shotInstance.GetComponent<Rigidbody>().velocity = fireForce * fireTransform.forward;
        //shotInstance.GetComponent<ParticleSystem>().Play();
        //fireParticle.Play();
        yield return new WaitForSeconds(fireRate - 0.25f);
        isShooting = false;
        yield return null;
        
    }

    IEnumerator MeeleHit()
    {
        isShooting = true;
        meeleHitbox.enabled = true;
        anim.SetBool("Range", false);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
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
}
