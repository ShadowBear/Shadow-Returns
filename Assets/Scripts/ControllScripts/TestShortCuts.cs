using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestShortCuts : MonoBehaviour {

    Transform WPParent;
    

    private void Start()
    {
        WPParent = GameObject.FindGameObjectWithTag("TestWP").transform;
    }

    // Update is called once per frame
    void Update () {
        //Alle Waffen
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GameObject weaponDepot = GameObject.FindGameObjectWithTag("WeaponDepot");
            GameObject weaponArm = GameObject.FindGameObjectWithTag("WeaponArm");

            print("Waffen ok");
            foreach (Weapon w in weaponDepot.GetComponentsInChildren<Weapon>(true))
            {
                w.gameObject.transform.SetParent(weaponArm.transform);
                //Adds the Weapon to all weapons
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().CollectWeapon(w.gameObject);
            }

        }
        //Leben Voll
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            print("Leben ok");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().ConsumeHealth(5000);
        }


        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(1);
            StartCoroutine(LevelChanged());
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            print(SceneManager.sceneCount);
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            {
                Debug.Log("Scene stimmt mit Level 1 überein");
                GameObject.FindGameObjectWithTag("Player").transform.position = WPParent.GetChild(0).position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            {
                Debug.Log("Scene stimmt mit Level 1 überein");
                GameObject.FindGameObjectWithTag("Player").transform.position = WPParent.GetChild(1).position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            {
                Debug.Log("Scene stimmt mit Level 1 überein");
                GameObject.FindGameObjectWithTag("Player").transform.position = WPParent.GetChild(2).position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SceneManager.LoadScene(2);
            StartCoroutine(LevelChanged());
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            print(SceneManager.sceneCount);
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                Debug.Log("Scene stimmt mit Level 2 überein");
                GameObject.FindGameObjectWithTag("Player").transform.position = WPParent.GetChild(0).position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                Debug.Log("Scene stimmt mit Level 2 überein");
                GameObject.FindGameObjectWithTag("Player").transform.position = WPParent.GetChild(1).position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                Debug.Log("Scene stimmt mit Level 2 überein");
                GameObject.FindGameObjectWithTag("Player").transform.position = WPParent.GetChild(2).position;
            }
        }

    }

    IEnumerator LevelChanged()
    {
        yield return new WaitForSeconds(1.5f);
        Start();
        yield return null;
    }
}
