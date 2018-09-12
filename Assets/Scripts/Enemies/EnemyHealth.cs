using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : HealthScript
{
    private ZombieAIController aiController;
    public float dashDistance = 2f;
    public LayerMask ground;
    private int EnemyID;

    new void Start()
    {
        aiController = GetComponent<ZombieAIController>();
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
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
        if(aiController != null) aiController.TakeHit();
    }

    protected override void Dying()
    {
        StartCoroutine(DieAnim());
        //Update Quest Status
        QuestLog.questLog.EnemyDied(EnemyID);        
    }

    IEnumerator DieAnim()
    {
        isDead = true;
        if (anim != null)
        {
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(3.5f);
        }        
        Destroy(gameObject);
        yield return null;
    }

    public void SetEnemyID(int ID)
    {
        EnemyID = ID;
    }
}