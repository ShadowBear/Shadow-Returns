using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    private Animator anim;
    private bool open = false;
    public GameObject chestItem;
    private bool looted = false;

    private Vector3 offset;

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
            if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
            {
                open = open ? false : true;
                if (anim) anim.SetBool("Open", open);
                if(open && !looted) StartCoroutine(InstantiateItem());
            }
        }
    }

    IEnumerator InstantiateItem()
    {
        looted = true;
        yield return new WaitForSeconds(0.75f);
        Instantiate(chestItem, transform.position + offset, Quaternion.identity);        
        yield return null;
    }

}
