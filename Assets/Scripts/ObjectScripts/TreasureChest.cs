using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    private Animator anim;
    private bool open = false;
    public GameObject chestItem;
    private bool looted = false;

    private float explosionRadius = 5;
    private int explosionForce = 250;

    private Vector3 offset;
    private bool antiPrell = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        offset.Set(0, 1, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (anim) anim.SetTrigger("Shake");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0 && !antiPrell)
            {
                StartCoroutine(AntiPrell());
                open = open ? false : true;
                if (anim) anim.SetBool("Open", open);
                if(open && !looted) StartCoroutine(InstantiateItem());
                
            }
        }
    }

    IEnumerator AntiPrell()
    {
        antiPrell = true;
        yield return new WaitForSeconds(1f);
        antiPrell = false;
        yield return null;

    }

    IEnumerator InstantiateItem()
    {
        looted = true;
        yield return new WaitForSeconds(0.75f);
        if (chestItem)
        {
            GameObject rewardObject = Instantiate(chestItem, transform.position + new Vector3(0,1,0), transform.rotation);
            if (rewardObject.GetComponent<Rigidbody>())
            {
                Vector3 randomExplosionVector = new Vector3(0, 0.9f, 0.5f);
                rewardObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position + randomExplosionVector, explosionRadius);
            }
        }
        yield return null;
    }

}
