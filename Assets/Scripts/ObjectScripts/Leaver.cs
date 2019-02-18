using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaver : MonoBehaviour {

    private bool isActivated = false;
    [SerializeField]
    private bool reaptingLever = false;
    private Animator animSelf;
    public GameObject[] toOpenObject;
    private bool moving = false;


    // Use this for initialization
    void Start () {
        animSelf = GetComponent<Animator>();
        animSelf.SetBool("isActivated", isActivated);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButton("Action"))
            {
                if (!moving)
                {
                    isActivated = isActivated ? false : true;
                    animSelf.SetBool("isActivated", isActivated);
                    foreach (GameObject child in toOpenObject)
                    {
                        if (child.GetComponent<TrapLever>() != null)
                        {
                            if (reaptingLever) child.GetComponent<TrapLever>().DeactivateTrap();
                            else child.GetComponent<TrapLever>().TriggerTrap();
                        }
                    }
                    StartCoroutine(WaitforNext());
                }
                
            }
        }
    }

    IEnumerator WaitforNext()
    {
        moving = true;
        yield return new WaitForSeconds(1f);
        moving = false;
        yield return null;
    }
}
