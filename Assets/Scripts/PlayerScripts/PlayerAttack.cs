using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject cursor;

    //true if u start without Weapons
    [SerializeField]
    private bool startNaked = false;

    [SerializeField]
    private PlayerMovement playerMovement;

    //Weapons
    //public GameObject[] weapons;
    private List<GameObject> weapons;
    public GameObject weaponAttackParent;
    private int weaponCount;

    
    public Transform fireTransform;
    public float fireForce = 5;
    public int ammuAmount;
    private int maxAmmu = 5;
    //private static float reloadTime = 1.467f;
    
    //Cursor Stuff
    private float cameraDistance;
    private Vector3 transformTo3D;

    public float fireRate = 0.5f;
    public float meleeAttackRate = 0.5f;

    private bool isAttacking = false;
    private bool isReloading = false;
    public bool fireStickDown = false;
    private bool isShielded = false;
    public PlayerHealth healthScript;

    public float distance;

    //Range AttackBoolen & Collider for Meele
    public bool rangeAttack;
    [SerializeField]
    private Collider meeleHitbox;
    public TrailRenderer trailRenderer;

    private Animator anim;
    public GameObject playerRotation;
    public ParticleSystem fireParticle;

    public GameObject sword;
    public GameObject gun;
    private GameObject equipedWeapon;

    PlayerRotation playRot;
    private Vector3 offset = new Vector3(0, 1, 0);
    public float distanceForCursor = 2.5f;

    //Sounds
    public AudioClip fireSound;
    public AudioClip[] swordSounds;
    private bool playingSound = false;

    //WeaponPackTest****************
    RaycastHit hit;
    public GameObject[] projectiles;
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float speed = 1000;

    [Header("GUI")]
    public GameObject swordIcon;
    public GameObject ammuIcon;





    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        equipedWeapon = GetComponentInChildren<Weapon>().gameObject;
        if (equipedWeapon.GetComponent<BoxCollider>())
        {
            meeleHitbox = equipedWeapon.GetComponent<BoxCollider>();
            meeleHitbox.enabled = false;
            ammuIcon.SetActive(false);
            swordIcon.SetActive(true);
            rangeAttack = false;
        }
        else
        {
            ammuIcon.SetActive(true);
            rangeAttack = true;
            swordIcon.SetActive(false);
        }
        if(trailRenderer) trailRenderer.enabled = false;
        
        ammuAmount = maxAmmu;

        int weaponsSize = weaponAttackParent.GetComponentsInChildren<Weapon>().Length;
        weapons = new List<GameObject>();
        //int i = 0;
        foreach(Weapon w in weaponAttackParent.GetComponentsInChildren<Weapon>())
        {
            weapons.Add(w.gameObject);
            w.gameObject.SetActive(false);
        }
        weapons[0].SetActive(true);

        if (startNaked) DontSuitUp();
        else
        {
            GameManager.control.SuitUp();
            SwapWeapon(1);
        }



        playRot = GetComponentInParent<PlayerRotation>();

    }
	
	// Update is called once per frame
	void Update () {
        
        if (Mathf.Abs((playerRotation.transform.position - playRot.GetCursorPos()).magnitude) < distanceForCursor)
            fireTransform.forward = playerRotation.transform.forward;
        else
        {
            fireTransform.LookAt(playRot.GetCursorPos() + offset);
        }

        CheckFired();

        //Change Weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0) SwapWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) SwapWeapon(-1);

        //************  TEST **************************//
        // Moveforward while Attacking has 2 be fixed //
        //if (isAttacking && !rangeAttack)
        //{
        //    //transform.parent.transform.parent.Translate(transform.parent.forward * Time.deltaTime*5);
        //}
        /********************************************/
    }

    private void DontSuitUp()
    {
        gun.SetActive(false);
        sword.SetActive(false);
        rangeAttack = false;
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i] = null;
        }
        anim.SetBool("Unarmed", true);
    }

    //public void AddWeapon(string type)
    //{
    //    if(type == "sword")
    //    {
    //        weapons[0] = sword;
    //        sword.SetActive(true);
    //        rangeAttack = false;
    //        //print("Sword added");
    //        anim.SetBool("Unarmed", false);
    //        anim.SetBool("Axe", true);
    //    }
    //    else if(type == "gun")
    //    {
    //        weapons[1] = gun;
    //        sword.SetActive(false);
    //        gun.SetActive(true);
    //        rangeAttack = true;
    //        anim.SetBool("Unarmed", false);
    //        anim.SetBool("Axe", false);
    //        anim.SetBool("Gun", true);
    //    }
    //    else if(type == "shield")
    //    {
    //        //can shield
    //    }
    //}

    //Only collects the new weapon to the weaponinventary
    public void CollectWeapon(GameObject weapon)
    {
        weapons.Add(weapon);
    }

    //Swaps the active weapon with the collected one
    public void CollectAndSwapWeapon(GameObject weapon)
    {
        weapons.Add(weapon);
        weapons.Remove(equipedWeapon);
        equipedWeapon.transform.SetParent(GameObject.FindGameObjectWithTag("WeaponDepot").transform);
        equipedWeapon.SetActive(false);
        equipedWeapon = weapon;
        if (equipedWeapon.GetComponent<BoxCollider>())
        {
            meeleHitbox = equipedWeapon.GetComponent<BoxCollider>();
            meeleHitbox.enabled = false;
        }
        equipedWeapon.SetActive(true);
    }

    private void CheckFired()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            isShielded = healthScript.isShielded;
            isReloading = weapons[weaponCount].GetComponent<Weapon>().GetReloadStatus();

            if (!isShielded)
            {
                if (rangeAttack)
                {
                    if (!isAttacking && !isReloading)
                    {
                        StartCoroutine(ArsenalShooting());
                    }
                }
                else StartCoroutine(MeeleHit());
            }             
        }
    }

    //new Shooting ******************************************************/
    IEnumerator ArsenalShooting()
    {
        if (isReloading || isAttacking) yield break;
        isAttacking = true;
        playerMovement.speed = playerMovement.GetSpeed(2);
        if (!anim.GetBool("Shooting"))
        {
            anim.SetBool("Shooting", true);
            yield return new WaitForSeconds(0.25f);
        }
        GetComponentInChildren<Weapon>().Shoot();
        
        yield return new WaitForSeconds(fireRate - 0.25f);
        isAttacking = false;
        //Test Speed slower
        playerMovement.speed = playerMovement.GetSpeed(1);
        //WaitTime before Stop Shooting
        yield return new WaitForSeconds(2.5f);
        //if (!isAttacking && !weapons[weaponCount].GetComponent<Weapon>().GetReloadStatus())
        if (!isAttacking)
        {         
            //AutoReload
            //GetComponentInChildren<Weapon>().ReloadReset();
            anim.SetBool("Shooting", false);
        }
        
        yield return null;
    }


    IEnumerator MeeleHit()
    {
        isAttacking = true;        
        //anim.SetBool("Range", false);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.25f);
        //meeleHitbox.enabled = true;
        if (trailRenderer) trailRenderer.enabled = true;

        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Slash(1)") && !playingSound) AudioSource.PlayClipAtPoint(swordSounds[1], transform.position);
        //else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Slash(2)") && !playingSound) AudioSource.PlayClipAtPoint(swordSounds[2], transform.position);
        //else if (!playingSound) AudioSource.PlayClipAtPoint(swordSounds[0], transform.position);
        //playingSound = true;
        //yield return new WaitForSeconds(0.75f);
        //playingSound = false;

        yield return null;

    }

    void SwapWeapon(int upOrDown)
    {
        weapons[weaponCount].SetActive(false);
        weaponCount = (weaponCount + upOrDown + weapons.Count) % weapons.Count;
        weapons[weaponCount].SetActive(true);
        equipedWeapon = weapons[weaponCount];
        if (weapons[weaponCount].GetComponent<BoxCollider>())
        {
            meeleHitbox = weapons[weaponCount].GetComponent<BoxCollider>();
            meeleHitbox.enabled = false;
        }        

        //New Swap
        anim.SetTrigger("SwitchWeapon");
        isAttacking = false;
        anim.SetBool("Shooting", false);
        Weapon.WeaponType weapon = weapons[weaponCount].GetComponent<Weapon>().GetProperties();
        if (weapon == Weapon.WeaponType.axe) {
            anim.SetBool("Axe", true);
            anim.SetBool("Gun", false);
            anim.SetBool("Sword", false);
            anim.SetBool("Unarmed", false);
            anim.SetBool("Range", false);
            rangeAttack = false;
            ammuIcon.SetActive(false);
            swordIcon.SetActive(true);
        }
        else if (weapon == Weapon.WeaponType.gun) {
            anim.SetBool("Gun", true);
            anim.SetBool("Axe", false);
            anim.SetBool("Sword", false);
            anim.SetBool("Unarmed", false);
            anim.SetBool("Range", true);
            rangeAttack = true;
            ammuIcon.SetActive(true);
            swordIcon.SetActive(false);
        }
        else if (weapon == Weapon.WeaponType.sword) {
            anim.SetBool("Sword", true);
            anim.SetBool("Axe", false);
            anim.SetBool("Gun", false);
            anim.SetBool("Unarmed", false);
            anim.SetBool("Range", false);
            rangeAttack = false;
            ammuIcon.SetActive(false);
            swordIcon.SetActive(true);
        }
        weapons[weaponCount].GetComponent<Weapon>().UpdateAmmuText();
    }

    public bool GetAttackStatus()
    {
        return isAttacking;
    }

    public bool GetReloadStatus()
    {
        isReloading = weapons[weaponCount].GetComponent<Weapon>().GetReloadStatus();
        return isReloading;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return playerMovement;
    }

    public Weapon GetEquipedWeapon()
    {
        return equipedWeapon.GetComponent<Weapon>();
    }

    public void TriggerHitbox(int i)
    {
        //Hitbox activate
        if (i == 1) meeleHitbox.enabled = true;
        else
        {
            meeleHitbox.enabled = false;
            isAttacking = false;
            if (trailRenderer) trailRenderer.enabled = false;
        }
    }

}
