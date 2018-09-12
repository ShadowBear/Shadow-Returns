using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    [SerializeField]
    GameObject ammuType;
    public int maxAmmu;
    private int ammuCount;
    private float reloadTime;
    private Animator anim;
    private bool isReloading = false;
    private PlayerAttack playerAttack;


    public Transform fireTransform;
    public int damageAmount;
    private int damageVarianz = 5;
    public float speed = 1000;

    public enum WeaponType { gun, sword, axe };

    public WeaponType weapon;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>();
        //Animation Lenght 3.3f
        reloadTime = 3.3f;
        ammuCount = maxAmmu;
        GameManager.control.ammuText.text = ammuCount.ToString();
    }

    public WeaponType GetProperties()
    {
        return weapon;
    }    

    public void Shoot()
    {
        if(ammuCount > 0)
        {
            ammuCount--;
            UpdateAmmuText();
            //print("Schuss");
            GameObject projectile = Instantiate(ammuType, fireTransform.position, Quaternion.identity) as GameObject;
            projectile.transform.rotation = fireTransform.rotation;
            projectile.GetComponent<PolygonArsenal.PolygonProjectileScript>().SetDamage(damageAmount + Random.Range(-damageVarianz, (damageVarianz+1)));
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
        }
        // AutoRealoding
        //else if(!isReloading)
        //{
        //    StartCoroutine(Reloading());
        //}
    }

    public void ReloadReset()
    {
        ammuCount = maxAmmu;
    }

    public void Reload()
    {
        if(ammuCount < maxAmmu && !isReloading) StartCoroutine(Reloading());
    }

    public int GetAmmuCount()
    {
        return ammuCount;
    }

    IEnumerator Reloading()
    {
        //anim.SetTrigger("Reload");
        isReloading = true;
        anim.SetBool("IsReloading", isReloading);
        yield return new WaitForSeconds(reloadTime);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(anim.GetLayerIndex("Reload-02")).Length);
        ammuCount = maxAmmu;
        GameManager.control.ammuText.text = ammuCount.ToString();
        isReloading = false;        
        anim.SetBool("IsReloading", isReloading);
        if (!playerAttack.GetAttackStatus()) anim.SetBool("Shooting", false);
        yield return null;
    }
    
    public bool GetReloadStatus()
    {
        return isReloading;
    }

    public void UpdateAmmuText()
    {
        if (ammuCount == 0) GameManager.control.ammuText.text = "R";
        else GameManager.control.ammuText.text = ammuCount.ToString();
    }
}
