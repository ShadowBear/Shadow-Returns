using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject shot;
    public GameObject light;
    public GameObject cursor;
    
    public Transform fireTransform;
    public float fireForce = 5;
    public int ammuAmount;
    public int maxAmmu = 3;
    private static float reloadTime = 1.467f;
    
    //Cursor Stuff
    private float cameraDistance;
    private Vector3 transformTo3D;

    public float fireRate = 0.5f;
    public bool isAttacking = false;
    public bool isReloading = false;
    public bool fireStickDown = false;
    private bool isShielded = false;
    public HealthScript healthScript;

    //Range AttackBoolen & Collider for Meele
    public bool rangeAttack = true;
    public Collider meeleHitbox;

    private Animator anim;
    public GameObject playerRotation;
    public ParticleSystem fireParticle;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        meeleHitbox.enabled = false;
        ammuAmount = maxAmmu;
        
    }
	
	// Update is called once per frame
	void Update () {
        CalculateCursorToShotTransform5();
        CheckFired();
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SwapWeapon();
        }
	}

    private void CheckFired()
    {
        
#if Unity_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            isShielded = healthScript.isShielded;
            if (!isAttacking && !isShielded && !isReloading)
            {
                if (rangeAttack)
                {
                    if (ammuAmount > 0)
                    {
                        ammuAmount--;
                        StartCoroutine(Shooting());    //StartCoroutine(Fire());
                    }
                }
                else StartCoroutine(MeeleHit());
            }            
        }
        if (Input.GetButtonDown("Fire2"))
        {
            isShielded = healthScript.isShielded;
            if (!isAttacking && !isShielded)
            {
                StartCoroutine(Light());
            }            
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
        GameObject shotInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation);
        shotInstance.GetComponent<Rigidbody>().velocity = fireForce * fireTransform.forward;
        //shotInstance.GetComponent<ParticleSystem>().Play();
        //fireParticle.Play();
        yield return new WaitForSeconds(fireRate - 0.25f);
        isAttacking = false;
        yield return null;
        
    }

    public float distance;

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
            shotInstance.GetComponent<Rigidbody>().velocity = fireForce * fireTransform.forward;
            //** TEST **//

            //Plane plane = new Plane(Camera.main.transform.forward, transform.position);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ////var dist : float;
            //float dist;
            //if (plane.Raycast(ray, out dist))
            //{
            //    Vector3 v3Pos = ray.GetPoint(dist);
            //    float distanceBetween = Vector3.Distance(v3Pos, transform.position);
            //    print("Distance: " + distanceBetween);
            //    v3Pos.y = 0;
            //    Vector3 temp = new Vector3(0, 1, 0);
            //    shotInstance.transform.LookAt(v3Pos);
            //    shotInstance.GetComponent<Rigidbody>().AddForce((shotInstance.transform.forward + temp) * 1000);
            //    //Debug.DrawRay(transform.position, v3Pos, Color.red);
            //}


            //distance = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
            //var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            //position = Camera.main.ScreenToWorldPoint(position);
            ////var go = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            //position.y = fireTransform.position.y;
            //shotInstance.transform.LookAt(position);
            //Debug.Log(position);
            //shotInstance.GetComponent<Rigidbody>().AddForce(shotInstance.transform.forward * 1000);


            //ammuAmount--;
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
        meeleHitbox.enabled = true;
        anim.SetBool("Range", false);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(fireRate);
        isAttacking = false;
        meeleHitbox.enabled = false;
        yield return null;

    }

    IEnumerator Light()
    {
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.25f);
        GameObject shotInstance = Instantiate(light, fireTransform.position, fireTransform.rotation);
        yield return null;

    }


    // Defines Cursor Pos in 3D World
    // Rotate Transform towards Cursor/Player.Forward
    // Depends on Distance between Cursor and Player
    void CalculateCursorToShotTransform()
    {
        cameraDistance = Mathf.Abs(cursor.transform.position.z - Camera.main.transform.position.z);
        transformTo3D = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance);
        transformTo3D = Camera.main.ScreenToWorldPoint(transformTo3D);
        //4.75 minDistance between Cursor and Player to Work well 
        if (Vector3.Distance(playerRotation.transform.position, transformTo3D) > 4.75f) fireTransform.LookAt(transformTo3D);
        else fireTransform.forward = playerRotation.transform.forward;
    }

    void CalculateCursorToShotTransform2()
    {
        Quaternion newRotation = Quaternion.LookRotation(transform.position - cursor.transform.position, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        fireTransform.rotation = Quaternion.Slerp(fireTransform.rotation, newRotation, Time.deltaTime * 8);
    }

    void CalculateCursorToShotTransform3()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = -AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        angle -= 90;

        //Ta Daaa
        fireTransform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }



    void CalculateCursorToShotTransform4()
    {
        Plane plane = new Plane(Camera.main.transform.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var dist : float;
        float dist;
        if (plane.Raycast(ray, out dist))
        {
            Vector3 v3Pos = ray.GetPoint(dist);
            float distanceBetween = Vector3.Distance(v3Pos, fireTransform.position);
            print("Distance: " + distanceBetween);
            v3Pos.y = 0;
            if (distanceBetween > 2.0f) fireTransform.LookAt(v3Pos);
            else fireTransform.forward = playerRotation.transform.forward;
            
        }
    }

    void CalculateCursorToShotTransform5()
    {
        fireTransform.forward = playerRotation.transform.forward;
    }

    void SwapWeapon()
    {
        rangeAttack = rangeAttack ? false : true;
    }

    public bool getAttackStatus()
    {
        return isAttacking;
    }
}
