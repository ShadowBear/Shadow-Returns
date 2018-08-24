using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField]
    GameObject ammuType;
    public int maxAmmu;
    private int ammuCount;
    public float reloadTime;
    private Animator anim;
    private bool isReloading = false;


    public Transform fireTransform;
    public int damageAmount;
    public float speed = 1000;

    public enum WeaponType { gun, sword, axe };

    public WeaponType weapon;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
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
            print("Schuss");
            GameObject projectile = Instantiate(ammuType, fireTransform.position, Quaternion.identity) as GameObject;
            projectile.transform.rotation = fireTransform.rotation;
            projectile.GetComponent<PolygonArsenal.PolygonProjectileScript>().SetDamage(damageAmount);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
        }
        else if(!isReloading)
        {
            StartCoroutine(Reloading());
        }
    }

    public void Reload()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        anim.SetTrigger("Reload");
        isReloading = true;
        // reloadTime Animtion + Puffer time 0.25
        yield return new WaitForSeconds(reloadTime + 0.25f);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(anim.GetLayerIndex("Reload-02")).Length);
        ammuCount = maxAmmu;
        isReloading = false;
        yield return null;
    }
}
