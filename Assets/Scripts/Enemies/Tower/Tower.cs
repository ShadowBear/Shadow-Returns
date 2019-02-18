using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tower : MonoBehaviour{

    public float fireRate = 1f;
    public float shotLifeTime = 5f;
    private bool isAttacking = false;
    public Transform shotTransform;
    public GameObject shot;
    public float fireForce = 5f;
    private int damage;
    private int ID;

    GameObject player;

    //JSON Externalisieren
    private string path;
    private string jsonString;
    public JSONEnemy enemy = new JSONEnemy();

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

        //JSON
        path = Application.streamingAssetsPath + "/JSON/EnemyData.json";
        jsonString = File.ReadAllText(path);
        enemy = JsonUtility.FromJson<JSONEnemy>(jsonString);

        ID = 15;
        damage = (int)(enemy.Turm[0] + enemy.Turm[2] * (0.5 * enemy.Turm[0]) - (0.5 * enemy.Turm[0]));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isAttacking)
            {
                StartCoroutine(RangeAttack());
            }
        }
    }

    IEnumerator RangeAttack()
    {
        isAttacking = true;
        shotTransform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
        GameObject shooted = Instantiate(shot, shotTransform.position, shotTransform.rotation);
        shooted.GetComponent<EnemyShotDamage>().SetDamage(damage);
        shooted.GetComponent<Rigidbody>().velocity = fireForce * shotTransform.forward;
        shooted.GetComponent<ParticleController>().lifeTime = shotLifeTime;
        yield return new WaitForSeconds(fireRate);
        isAttacking = false;
        yield return null;
    }
}
