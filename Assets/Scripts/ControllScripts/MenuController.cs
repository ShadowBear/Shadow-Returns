using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject dialogMenu;
    public Text dialogText;
    public Button dialogButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleWindow(pauseMenu);

    }


    public void StartBtn()
    {
        SceneManager.LoadScene("BasicTestScene");
    }


    public void ContinueBtn()
    {
        ToggleWindow(pauseMenu);
    }

    public void QuitBtn()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void SettingsBtn()
    {

    }

    public void LoadBtn()
    {

    }

    public void Dialog()
    {
        ToggleWindow(dialogMenu);
    }

    public void SetDialogText(string dialog)
    {
        dialogText.text = dialog;
    }

    void ToggleWindow(GameObject window)
    {
        print("WechsleFenster");
        print("TimeScale: " + Time.timeScale);
        if (window.activeSelf)
        {
            Time.timeScale = 1;
            window.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            window.SetActive(true);
        }
    }

}
