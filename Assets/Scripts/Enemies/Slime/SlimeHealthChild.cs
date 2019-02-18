using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealthChild : HealthScript
{
    public GameObject slime;
    private SlimeAI slimeAI;

    [SerializeField]
    private int startLifeState = 3;
    private int lifeState = 3;

    public BoxCollider hitBox;
    public float dashDistance = 2f;
    public LayerMask ground;
    
    new void Start()
    {
        base.Start();
        slimeAI = GetComponent<SlimeAI>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
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

    protected override void TakeHit(bool melee)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            //GetComponent<Rigidbody>().AddForce(-transform.forward * pushForce);

            //PushBack Distance depends on Attacktyp
            float pushBackRange = 0;
            if (melee) pushBackRange = dashDistance;
            else pushBackRange = dashDistance * 0.1f;

            Vector3 dashDirection = player.GetComponentInChildren<PlayerRotation>().transform.forward;
            Vector3 dashVector = dashDirection * pushBackRange;

            Vector3 targetPosition;
            Ray dashRay = new Ray(transform.position + GetComponent<Rigidbody>().centerOfMass, dashDirection);
            RaycastHit rayHit;
            if (Physics.Raycast(dashRay, out rayHit, pushBackRange, ground.value))
            {
                targetPosition = rayHit.point;
            }
            else targetPosition = transform.position + dashVector;

            transform.SetPositionAndRotation(targetPosition, transform.rotation);
        }
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