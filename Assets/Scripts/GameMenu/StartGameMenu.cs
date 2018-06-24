using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void ButtonStart()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ButtonSettings()
    {
        //Settings Todo
    }

    public void ButtonLoad()
    {
        //Load Game Todo
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
