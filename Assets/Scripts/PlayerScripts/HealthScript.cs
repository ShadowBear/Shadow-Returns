using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public int maxHealth = 100;
    protected float health;
    public Image healthbar;
    public bool isShielded = false;
    //public Shield playerShieldScript;

    public bool useRelativeRotation = true;       // Use relative rotation should be used for this gameobject?
    public Transform relativeRotationTransform;          // The local rotatation at the start of the scene.
    public Quaternion relativeRotation;

    //Header Beispiel für bessere Übersicht im Inspector
    [Header ("DMG")]
    public bool isDead = false;

    //Test Vererbung ************ //
    //public Image dmgFrame;
    
    /************************************/

    protected Animator anim;
    private Vector3 rotationOffset;
    protected GameObject player;

    protected void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        if(healthbar != null) healthbar.fillAmount = health / maxHealth;
        if(relativeRotationTransform != null) relativeRotation = relativeRotationTransform.rotation;
        anim = GetComponent<Animator>();
        isDead = false;
        //if (dmgFrame != null) dmgFrame.CrossFadeAlpha(0, 0.1f, false);
    }

    // Update is called once per frame
    protected void Update () {
		if(health <= 0 && !isDead)
        {
            Dying();
        }
        if (useRelativeRotation) RotateHealthbar();
        //if (useRelativeRotation) relativeRotationTransform.rotation = relativeRotation;
    }

    public void SetHealth(int value)
    {
        maxHealth = value;
        Start();
        //print(health);
    }

    public void TakeDamage(float damage)
    {
        if (!isDead && !isShielded)
        {
            //print("DamageTaken");
            
            health -= damage;
            if(healthbar != null) healthbar.fillAmount = (float)health / maxHealth;
            if(anim != null) anim.SetTrigger("damaged");
            //if (gameObject == GameObject.FindGameObjectWithTag("Player")) StartCoroutine(DMGFrame());
            //else 
            if(damage > 1) GameManager.control.ShowDmgText(damage, transform);
        }
        //else if(!isDead && isShielded)
        //{
        //    if (playerShieldScript != null) playerShieldScript.TakeDMG(damage);
        //}
    }

    protected virtual void Dying()
    {
        print("DieParentHealth!!");
        //if (GameObject.FindGameObjectWithTag("Player") != gameObject) StartCoroutine(DieAnim());
        StartCoroutine(DieAnim());
    }

    IEnumerator DieAnim()
    {
        isDead = true;
        if (anim != null)
        {
            anim.SetBool("die", true);
            yield return new WaitForSeconds(1.2f);
        }
        if (GetComponent<HidingGhostTomb>() != null) GetComponent<HidingGhostTomb>().Die();
        Destroy(gameObject);
        if (GetComponent<DropRate>()) GetComponent<DropRate>().DropItem();
        yield return null;   
    }

    //IEnumerator DMGFrame()
    //{
    //    if (dmgFrame)
    //    {
    //        //dmgFrame.enabled = true;
    //        dmgFrame.CrossFadeAlpha(1, 0.5f, false);
    //        yield return new WaitForSeconds(0.5f);
    //        //dmgFrame.enabled = false;
    //        dmgFrame.CrossFadeAlpha(0, 0.5f, false);            
    //    }
    //    yield return null;
    //}


    public void RotateHealthbar()
    {
        rotationOffset = Camera.main.transform.position;
        rotationOffset.y = player.transform.position.y;
        relativeRotationTransform.LookAt(rotationOffset);
        relativeRotationTransform.forward = -relativeRotationTransform.forward;
    }
}