using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {

    public int maxHealth = 100;
    protected float health;
    public Image healthbar;
    public bool isShielded = false;
    
    //Header Beispiel für bessere Übersicht im Inspector
    [Header ("DMG")]
    public bool isDead = false;

    public float pushForce = 50;

    [SerializeField]
    protected float startHitDelay = 0.75f;
    protected float hitDelay = 0;
    [SerializeField]
    protected bool hitable = true;

    protected Animator anim;
    private Vector3 rotationOffset;
    protected GameObject player;

    protected void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        if(healthbar != null) healthbar.fillAmount = health / maxHealth;
        anim = GetComponentInChildren<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    protected void Update () {
        if (hitDelay > 0)
        {
            hitDelay -= Time.deltaTime;
            if (hitDelay <= 0) hitable = true;
        }
    }

    public void SetHealth(int value)
    {
        maxHealth = value;
        Start();
    }
    public virtual void TakeDamage(float damage)
    {
        
        if (!isDead && !isShielded)
        {
            hitable = false;
            hitDelay = startHitDelay;
            TakeHit();

            health -= damage;
            if (healthbar != null) healthbar.fillAmount = (float)health / maxHealth;
            if (anim != null) anim.SetTrigger("damaged");
            if (damage > 1) GameManager.control.ShowDmgText(damage, transform);

            if (health <= 0 && !isDead)
            {
                Dying();
            }
        }
    }

    public virtual void TakeDamage(float damage, bool melee)
    {
        if (!hitable) return;
        if (!isDead && !isShielded)
        {
            hitable = false;
            hitDelay = startHitDelay;
            TakeHit(melee);
            
            health -= damage;
            if(healthbar != null) healthbar.fillAmount = (float)health / maxHealth;
            if(anim != null) anim.SetTrigger("damaged");
            if(damage > 1) GameManager.control.ShowDmgText(damage, transform);

            //Pushing Back On Hit
            //if (GetComponent<Rigidbody>() != null) GetComponent<Rigidbody>().AddForce(-transform.forward * pushForce);

            if (health <= 0 && !isDead)
            {
                Dying();
            }
        }
    }




    protected virtual void TakeHit() { }
    protected virtual void TakeHit(bool melee) { }

    

    protected virtual void Dying()
    {
        //print("DieParentHealth!!");
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
        //if (GetComponent<DropRate>()) GetComponent<DropRate>().DropItem();
        yield return null;   
    }


    //public void RotateHealthbar()
    //{
    //    rotationOffset = Camera.main.transform.position;
    //    rotationOffset.y = player.transform.position.y;
    //    relativeRotationTransform.LookAt(rotationOffset);
    //    relativeRotationTransform.forward = -relativeRotationTransform.forward;
    //}
}