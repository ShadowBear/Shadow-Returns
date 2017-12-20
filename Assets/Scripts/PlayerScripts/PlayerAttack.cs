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

    private Animator anim;
    public ParticleSystem fireParticle;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckFired();
	}

    private void CheckFired()
    {
        if(Input.GetButtonDown("Fire1") && !isShooting) StartCoroutine(Fire());
        if (Input.GetButtonDown("Fire2")) StartCoroutine(Light());
    }

    IEnumerator Fire()
    {
        isShooting = true;
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

    IEnumerator Light()
    {
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.25f);
        GameObject shotInstance = Instantiate(light, fireTransform.position, fireTransform.rotation);
        yield return null;

    }
}
