using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthScript {

    //public bool useRelativeRotation = true;       // Use relative rotation should be used for this gameobject?
    //public Transform relativeRotationTransform;          // The local rotatation at the start of the scene.
    //public Quaternion relativeRotation;
    
    //private Vector3 rotationOffset;

    //Header Beispiel für bessere Übersicht im Inspector
    [Header ("DMG")]
    public Image dmgFrame;
    public Shield playerShieldScript;

    new void Start () {
   
        if (dmgFrame != null) dmgFrame.CrossFadeAlpha(0, 0.1f, false);
        base.Start();
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }

    protected override void Dying()
    {
        print("DiePlayer!!");
        StartCoroutine(DieAnim());
    }

    new public void TakeDamage(float damage)
    {
        if (!isDead && !isShielded)
        {
            health -= damage;
            if (healthbar != null) healthbar.fillAmount = (float)health / maxHealth;
            if (anim != null) anim.SetTrigger("damaged");
            StartCoroutine(DMGFrame());
        }
        else if (!isDead && isShielded)
        {
            if (playerShieldScript != null) playerShieldScript.TakeDMG(damage);
        }
    }

    IEnumerator DieAnim()
    {
        isDead = true;
        if (anim != null)
        {
            anim.SetBool("die", true);
            yield return new WaitForSeconds(1.2f);
        }

        yield return null;   
    }

    IEnumerator DMGFrame()
    {
        if (dmgFrame)
        {
            //dmgFrame.enabled = true;
            dmgFrame.CrossFadeAlpha(1, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
            //dmgFrame.enabled = false;
            dmgFrame.CrossFadeAlpha(0, 0.5f, false);            
        }
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