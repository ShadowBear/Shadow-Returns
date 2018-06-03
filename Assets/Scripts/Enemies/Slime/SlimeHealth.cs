using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 30;
    private float health;
    public Image healthbar;

    public bool useRelativeRotation = true;       // Use relative rotation should be used for this gameobject?
    public Transform relativeRotationTransform;          // The local rotatation at the start of the scene.
    public Quaternion relativeRotation;
    public GameObject slime;

    [SerializeField]
    private int lifeState = 3;

    //Header Beispiel für bessere Übersicht im Inspector
    [Header("DMG")]
    public bool isDead = false;

    private Animator anim;


    void Start()
    {
        health = maxHealth;
        healthbar.fillAmount = health / maxHealth;
        relativeRotation = relativeRotationTransform.rotation;
        anim = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0 && !isDead)
        //{
        //    StartCoroutine(Die());
        //}
        //if (useRelativeRotation) relativeRotationTransform.rotation = relativeRotation;
        if (useRelativeRotation)
        {
            Vector3 cam = new Vector3(Camera.main.transform.position.x,0,Camera.main.transform.position.z);
            relativeRotationTransform.LookAt(cam);            
        }
    }

    public void SetHealth(int value)
    {
        maxHealth = value;
        Start();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            healthbar.fillAmount = (float)health / maxHealth;
            if (damage > 1) GameManager.control.ShowDmgText(damage, transform);
            //if (anim != null) anim.SetTrigger("damaged");
        }
    }

    public void SetLiveState(int life)
    {
        lifeState = life;
    }
       

    IEnumerator Die()
    {
        isDead = true;
        anim.SetBool("die", true);
        yield return new WaitForSeconds(1.2f);
        if (lifeState > 1)
        {
            lifeState--;
            // Anzahl Kinder Berechnet sich aus Anzahl der Spaltungen Standart 3 
            // und es werden immer doppelt soviele Kinder wie vorherige Runde
            int numberKids = (3 - lifeState) * 2;
            for (int i = 0; i < numberKids; i++)
            {
                GameObject slimeKid = Instantiate(slime, transform.position, transform.rotation);
                slimeKid.GetComponent<SlimeHealth>().SetLiveState(lifeState);
                slimeKid.GetComponent<SlimeHealth>().SetHealth(maxHealth / 2);
                //Größe um ein drittel verringern
                slimeKid.transform.localScale = slimeKid.transform.localScale * 0.66f;
            }
        }
        Destroy(gameObject);
        yield return null;
    }
}