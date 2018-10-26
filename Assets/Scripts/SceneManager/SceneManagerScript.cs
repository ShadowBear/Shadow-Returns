using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

    private bool AntiPrell = false;
    public int NextSceneToLoad;
    

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetButtonDown("Action") && !AntiPrell)
        {
            AntiPrell = true;
            //Debug.Log("ChangeScene");
            SceneManager.LoadScene(NextSceneToLoad);
        }
    }


}
