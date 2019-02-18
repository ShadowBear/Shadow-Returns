using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour {

    public string weaponToCollectID;
    private GameObject weaponDepot;
    private GameObject weaponArm;

    private void Start()
    {
        weaponDepot = GameObject.FindGameObjectWithTag("WeaponDepot");
        weaponArm = GameObject.FindGameObjectWithTag("WeaponArm");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetButtonDown("Action"))
            {
#elif UNITY_ANDROID
            if (AndroidActionButton.androidActionButton.clicked )
            {
#endif
                //Debug.Log("ActionButton");
                foreach (Weapon w in weaponDepot.GetComponentsInChildren<Weapon>(true))
                {
                    if (w.weaponID == weaponToCollectID)
                    {
                        w.gameObject.transform.SetParent(weaponArm.transform);

                        //Adds the Weapon to all weapons
                        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().CollectWeapon(w.gameObject);
                        
                        //For Swaping Weapon old weapon gets lost
                        //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().CollectAndSwapWeapon(w.gameObject);
                        //Debug.Log("ActionButton");
                        Destroy(gameObject);
                    }
                    
                }
            }
        }
    }
}
