using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class PlayerHealth : HealthScript {

     //private Vector3 rotationOffset;

    //Header Beispiel für bessere Übersicht im Inspector
    [Header ("DMG")]
    public Image dmgFrame;
    public PostProcessingProfile processingProfile;
    public Shield playerShieldScript;
    [SerializeField]
    private Renderer[] playerRenderer;
    private Renderer weaponRenderer;

    [SerializeField]
    private GameObject gameOverText;
    private float hitDelayTime = 1.75f;

    new void Start () {
   
        if (dmgFrame != null) dmgFrame.CrossFadeAlpha(0, 0.1f, false);
        startHitDelay = hitDelayTime;
        processingProfile.vignette.enabled = false;
        base.Start();        
        
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }

    protected override void Dying()
    {
        isDead = true;
        StartCoroutine(DieAnim());
    }

    new public void TakeDamage(float damage)
    {
        if (!hitable) return;
        hitable = false;
        hitDelay = startHitDelay;

        if (!isDead && !isShielded)
        {
            health -= damage;
            if (healthbar != null) healthbar.fillAmount = health / maxHealth;
            if (anim != null) anim.SetTrigger("damaged");
            StartCoroutine(DMGFrame());
            StartCoroutine(Blinking());
        }
        else if (!isDead && isShielded)
        {
            if (playerShieldScript != null) playerShieldScript.TakeDMG(damage);
        }
        if (health <= 0 && !isDead) Dying();
    }

    public void DrinkPotion(int potionValue)
    {
        if (GameManager.control.potionNmbr > 0)
        {
            if (health >= maxHealth) return;
            health = (health + potionValue) > maxHealth ? maxHealth : (health + potionValue);
            if (healthbar != null) healthbar.fillAmount = health / maxHealth;
            GameManager.control.DrinkPotion();
        }
    }

    IEnumerator DieAnim()
    {        
        if (anim != null)
        {
            anim.SetBool("Dead", true);
            yield return new WaitForSeconds(3.2f);
        }
        gameOverText.SetActive(true);
        Time.timeScale = 0;
        yield return null;   
    }

    IEnumerator Blinking()
    {
        weaponRenderer = GameObject.FindGameObjectWithTag("WeaponArm").GetComponentInChildren<Renderer>();
        for (int i = 0; i < 8; i++)
        {
            for (int n = 0; n < playerRenderer.Length; n++)
            {
                playerRenderer[n].enabled = false;
                weaponRenderer.enabled = false; 
            }
            yield return new WaitForSeconds(0.1f);
            for (int n = 0; n < playerRenderer.Length; n++)
            {
                playerRenderer[n].enabled = true;
                weaponRenderer.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    IEnumerator DMGFrame()
    {

        if (dmgFrame)
        {
            dmgFrame.CrossFadeAlpha(1, 0.5f, false);
            yield return new WaitForSeconds(0.75f);
            dmgFrame.CrossFadeAlpha(0, 0.5f, false);
        }

        //Vignitte Testing *********************//
        //processingProfile.vignette.enabled = true;
        //VignetteModel.Settings settings = processingProfile.vignette.settings;
        //settings.intensity = 0.6f;
        //yield return new WaitForSeconds(1.5f);
        //settings.intensity = 0.2f;
        //yield return new WaitForSeconds(1.5f);
        //processingProfile.vignette.enabled = false;

        yield return null;
    }

}