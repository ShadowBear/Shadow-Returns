using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EnemyHealth : HealthScript
{
    private ZombieAIController aiController;
    public float dashDistance = 2f;
    public LayerMask ground;
    [SerializeField]
    private int EnemyID;

    //JSON Externalisieren
    private string path;
    private string jsonString;
    public JSONEnemy enemyHealth = new JSONEnemy();

    new void Start()
    {
        aiController = GetComponent<ZombieAIController>();
        base.Start();

        //JSON
        path = Application.streamingAssetsPath + "/JSON/EnemyData.json";
        jsonString = File.ReadAllText(path);
        enemyHealth = JsonUtility.FromJson<JSONEnemy>(jsonString);

        switch (EnemyID){
            case 1: health = enemyHealth.Golem[1] * enemyHealth.Golem[2];
                maxHealth = enemyHealth.Golem[1] * enemyHealth.Golem[2];
                break;
            case 2: health = enemyHealth.Kaefer[1] * enemyHealth.Kaefer[2];
                maxHealth = enemyHealth.Kaefer[1] * enemyHealth.Kaefer[2];
                break;
            case 3: health = enemyHealth.Geist[1] * enemyHealth.Geist[2];
                maxHealth = enemyHealth.Geist[1] * enemyHealth.Geist[2];
                break;
            case 4: health = enemyHealth.Magma[1] * enemyHealth.Magma[2];
                maxHealth = enemyHealth.Magma[1] * enemyHealth.Magma[2];
                break;
            case 5: health = enemyHealth.Zombie[1] * enemyHealth.Zombie[2];
                maxHealth = enemyHealth.Zombie[1] * enemyHealth.Zombie[2];
                break;
            case 6: health = enemyHealth.Schleim[1] * enemyHealth.Schleim[2];
                maxHealth = enemyHealth.Schleim[1] * enemyHealth.Schleim[2];
                break;
            case 7: health = enemyHealth.Feuerschleim[1] * enemyHealth.Feuerschleim[2];
                maxHealth = enemyHealth.Feuerschleim[1] * enemyHealth.Feuerschleim[2];
                break;
            case 8: health = enemyHealth.Wolf[1] * enemyHealth.Wolf[2];
                maxHealth = enemyHealth.Wolf[1] * enemyHealth.Wolf[2];
                break;
            case 9: health = enemyHealth.Wildschwein[1] * enemyHealth.Wildschwein[2];
                maxHealth = enemyHealth.Wildschwein[1] * enemyHealth.Wildschwein[2];
                break;
            case 10: health = enemyHealth.Hase[1] * enemyHealth.Hase[2];
                maxHealth = enemyHealth.Hase[1] * enemyHealth.Hase[2];
                break;
            case 11: health = enemyHealth.Spinne[1] * enemyHealth.Spinne[2];
                maxHealth = enemyHealth.Spinne[1] * enemyHealth.Spinne[2];
                break;
            case 15: health = enemyHealth.Turm[1] * enemyHealth.Turm[2];
                maxHealth = enemyHealth.Turm[1] * enemyHealth.Turm[2];
                break;
            default: health = 50;
                maxHealth = 50;
                break;
        }
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
            anim.SetBool("Dead", true);
            yield return new WaitForSeconds(3.5f);
            if (GetComponent<DropRate>())
            {
                GetComponent<DropRate>().DropItem();
            }
        }        
        Destroy(gameObject);
        yield return null;
    }

    public void SetEnemyID(int ID)
    {
        if(EnemyID == 0) EnemyID = ID;
    }

}