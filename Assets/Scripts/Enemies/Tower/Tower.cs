using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float fireRate = 1f;
    public GameObject shot;
    public float fireForce = 5f;
    public float shotLifeTime = 5f;
    public Transform shotTransform;

    private GameObject player;
    private bool isAttacking;

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	

	// Update is called once per frame
	void Update () {
        if (!isAttacking)
        {
            StartCoroutine(RangeAttack());
        }
	}

    IEnumerator RangeAttack()
    {
        isAttacking = true;
        shotTransform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
        GameObject shooted = Instantiate(shot, shotTransform.position, shotTransform.rotation);
        shooted.GetComponent<Rigidbody>().velocity = fireForce * shotTransform.forward;
        shooted.GetComponent<ParticleController>().lifeTime = shotLifeTime;
        yield return new WaitForSeconds(fireRate);
        isAttacking = false;
        yield return null;
    }
}
