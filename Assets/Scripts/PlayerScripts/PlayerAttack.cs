using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject shot;
    public GameObject cursor;

    //true if u start without Weapons
    [SerializeField]
    private bool startNaked = false;

    //Weapons
    public GameObject[] weapons;
    private int weaponCount;

    
    public Transform fireTransform;
    public float fireForce = 5;
    public int ammuAmount;
    private int maxAmmu = 5;
    private static float reloadTime = 1.467f;
    
    //Cursor Stuff
    private float cameraDistance;
    private Vector3 transformTo3D;

    public float fireRate = 0.5f;
    public float meleeAttackRate = 0.5f;

    public bool isAttacking = false;
    public bool isReloading = false;
    public bool fireStickDown = false;
    private bool isShielded = false;
    public PlayerHealth healthScript;

    public float distance;

    //Range AttackBoolen & Collider for Meele
    public bool rangeAttack;
    public Collider meeleHitbox;

    private Animator anim;
    public GameObject playerRotation;
    public ParticleSystem fireParticle;

    public GameObject sword;
    public GameObject gun;

    //PlayerRotation playRot;
    //private Vector3 offset = new Vector3(0, 1, 0);

    //Sounds
    public AudioClip fireSound;
    public AudioClip[] swordSounds;
    private bool playingSound = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        meeleHitbox.enabled = false;
        rangeAttack = true;
        ammuAmount = maxAmmu;

        if (startNaked) DontSuitUp();
        else GameManager.control.SuitUp();

        //playRot = GetComponentInParent<PlayerRotation>();

    }
	
	// Update is called once per frame
	void Update () {

        //if(anim.GetBool("Fire") == false) { 
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack Done")) {
            print("FertigAngreifen");
            isAttacking = false;
            meeleHitbox.enabled = false;
        }
        
        //old
        fireTransform.forward = playerRotation.transform.forward;
        
        //fireTransform.LookAt(playRot.GetCursorPos() + offset);

        if (sword.activeSelf || gun.activeSelf) CheckFired();
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && GameManager.control.swordCollected && GameManager.control.gunCollected)
        {
            SwapWeapon();
        }
	}

    private void DontSuitUp()
    {
        gun.SetActive(false);
        sword.SetActive(false);
        rangeAttack = false;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = null;
        }
    }

    public void AddWeapon(string type)
    {
        if(type == "sword")
        {
            weapons[0] = sword;
            sword.SetActive(true);
            rangeAttack = false;
            print("Sword added");
        }
        else if(type == "gun")
        {
            weapons[1] = gun;
            sword.SetActive(false);
            gun.SetActive(true);
            rangeAttack = true;
        }
        else if(type == "shield")
        {
            //can shield
        }
    }


    private void CheckFired()
    {
        
#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            isShielded = healthScript.isShielded;
            if (rangeAttack)
            {
                if (!isAttacking && !isShielded && !isReloading)
                {
                    if (ammuAmount > 0)
                    {
                        ammuAmount--;
                        StartCoroutine(Shooting());    //StartCoroutine(Fire());
                    }
                }                
            }
            else if(!isShielded) StartCoroutine(MeeleHit());
        }
#else
        if (fireStickDown && rangeAttack && !isShooting) StartCoroutine(Fire());
        else if(fireStickDown && !rangeAttack && !isShooting) StartCoroutine(MeeleHit());
#endif
    }

    IEnumerator Fire()
    {
        isAttacking = true;
        anim.SetBool("Range", true);
        anim.SetTrigger("Fire");
        
       
        yield return new WaitForSeconds(0.25f);
        GetComponent<AudioSource>().Play();
        GameObject shotInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation);
        shotInstance.GetComponent<Rigidbody>().velocity = fireForce * fireTransform.forward;
        //shotInstance.GetComponent<ParticleSystem>().Play();
        //fireParticle.Play();
        yield return new WaitForSeconds(fireRate - 0.25f);
        GetComponent<AudioSource>().Stop();
        isAttacking = false;
        yield return null;
        
    }

    

    IEnumerator Shooting()
    {
        if (isReloading || isAttacking) yield break;
        isAttacking = true;
        if (!anim.GetBool("Shooting")) {
            anim.SetBool("Range", true);
            anim.SetBool("Shooting", true);
            yield return new WaitForSeconds(0.25f);
        }
        if (!isReloading)
        {
            GameObject shotInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation);
            AudioSource.PlayClipAtPoint(fireSound, fireTransform.position);
            //shotInstance.GetComponent<Rigidbody>().velocity = fireForce * fireTransform.forward;
            shotInstance.GetComponent<Rigidbody>().AddForce(shotInstance.transform.forward * 1000);
        }
        if(ammuAmount <= 0 && !isReloading)
        {
            anim.SetTrigger("Reload");
            isReloading = true;
            // reloadTime Animtion + Puffer time 0.25
            yield return new WaitForSeconds(reloadTime + 0.25f);
            //yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(anim.GetLayerIndex("Reload-02")).Length);
            ammuAmount = maxAmmu;
            isReloading = false;
        }
        yield return new WaitForSeconds(fireRate - 0.25f);
        isAttacking = false;
        //WaitTime before Stop Shooting
        yield return new WaitForSeconds(0.5f);
        if (!isAttacking)
        {
            anim.SetBool("Shooting", false);
            ammuAmount = maxAmmu;
        }
        yield return null;
    }

    IEnumerator MeeleHit()
    {
        isAttacking = true;
        
        //print("MeleeActive");
        anim.SetBool("Range", false);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.25f);
        meeleHitbox.enabled = true;
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Slash(1)") && !playingSound) AudioSource.PlayClipAtPoint(swordSounds[1], transform.position);
        //else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Slash(2)") && !playingSound) AudioSource.PlayClipAtPoint(swordSounds[2], transform.position);
        //else if (!playingSound) AudioSource.PlayClipAtPoint(swordSounds[0], transform.position);
        //playingSound = true;
        //yield return new WaitForSeconds(0.75f);
        //playingSound = false;
            //Before Animation Controlled Finish
            //yield return new WaitForSeconds(meleeAttackRate-0.25f);
            //isAttacking = false;
            //meeleHitbox.enabled = false;
            ////print("Meleefinish");
            yield return null;

    }

    void SwapWeapon()
    {
        weapons[weaponCount].SetActive(false);
        weaponCount = (weaponCount+1) % weapons.Length;
        weapons[weaponCount].SetActive(true);
        rangeAttack = rangeAttack ? false : true;
        anim.SetBool("Range", rangeAttack);
    }

    public bool GetAttackStatus()
    {
        return isAttacking;
    }
}
