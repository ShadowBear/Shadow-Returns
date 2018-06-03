using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealthChild : HealthScript
{

    //[SerializeField]
    //private float health;
    
    public GameObject slime;
    private SlimeAI slimeAI;

    [SerializeField]
    private int startLifeState = 3;
    private int lifeState = 3;

    public BoxCollider hitBox;

    //private Animator anim;

    new void Start()
    {
        base.Start();
        slimeAI = GetComponent<SlimeAI>();
    }

    // Update is called once per frame
    new void Update()
    {
        //if (health <= 0 && !isDead)
        //{
        //    StartCoroutine(Die());
        //}
        base.Update();
        //if (useRelativeRotation) relativeRotationTransform.rotation = relativeRotation;
        if (useRelativeRotation)
        {
            Vector3 cam = new Vector3(Camera.main.transform.position.x,0,Camera.main.transform.position.z);
            relativeRotationTransform.LookAt(cam);            
        }
    }
       
    public void SetLifeState(int life, int startLife)
    {
        startLifeState = startLife;
        lifeState = life;
    }

    protected override void Dying()
    {
        print("DieChild");
        StartCoroutine(Die());
    }

    protected override void TakeHit()
    {
        slimeAI.TakeHit();
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
            int numberKids = (startLifeState - lifeState) * 2;
            for (int i = 0; i < numberKids; i++)
            {
                GameObject slimeKid = Instantiate(slime, transform.position, Quaternion.identity);
                slimeKid.GetComponent<SlimeHealthChild>().SetLifeState(lifeState,startLifeState);
                slimeKid.GetComponent<SlimeHealthChild>().SetHealth(maxHealth / 2);
               
                //Größe um ein drittel verringern
                slimeKid.transform.localScale *= 0.66f;

                //hitbox Anpassen für Trefferchance
                slimeKid.GetComponent<SlimeHealthChild>().hitBox.size = new Vector3(hitBox.size.x, hitBox.size.y*1.33f, hitBox.size.z);
                hitBox.center = new Vector3(hitBox.center.x, hitBox.center.y * 1.33f, hitBox.center.z);
            }
        }
        Destroy(gameObject);
        yield return null;
    }
}