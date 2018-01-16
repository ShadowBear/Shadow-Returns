using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    private int maxHealth = 100;
    private float health;
    public Image healthbar;
    public bool isShielded = false;
    public Shield playerShieldScript;

    public bool useRelativeRotation = true;       // Use relative rotation should be used for this gameobject?
    public Transform relativeRotationTransform;          // The local rotatation at the start of the scene.
    public Quaternion relativeRotation;

    //Header Beispiel für bessere Übersicht im Inspector
    [Header ("DMG")]
    public bool isDead = false;
    public Image dmgFrame;

    private Animator anim;


    void Start () {
        health = maxHealth;
        healthbar.fillAmount = health / maxHealth;
        relativeRotation = relativeRotationTransform.rotation;
        anim = GetComponent<Animator>();
        isDead = false;
        if (dmgFrame != null) dmgFrame.CrossFadeAlpha(0, 0.1f, false);
    }
	
	// Update is called once per frame
	void Update () {
		if(health <= 0 && !isDead)
        {
            Dying();
        }
        if (useRelativeRotation) relativeRotationTransform.rotation = relativeRotation;
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
            healthbar.fillAmount = (float)health / maxHealth;
            if(anim != null) anim.SetTrigger("damaged");
            if (gameObject == GameObject.FindGameObjectWithTag("Player")) StartCoroutine(DMGFrame());
        }
        else if(!isDead && isShielded)
        {
            playerShieldScript.TakeDMG(damage);
        }
    }

    void Dying()
    {
        //print("Die!!");
        if (GameObject.FindGameObjectWithTag("Player") != gameObject) StartCoroutine(DieAnim());
    }

    IEnumerator DieAnim()
    {
        isDead = true;
        anim.SetBool("die", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
        yield return null;   
    }

    IEnumerator DMGFrame()
    {
        //dmgFrame.enabled = true;
        dmgFrame.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        //dmgFrame.enabled = false;
        dmgFrame.CrossFadeAlpha(0, 0.5f, false);
        yield return null;
    }
}