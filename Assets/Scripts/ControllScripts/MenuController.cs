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
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleWindow(pauseMenu);        

    }


    public void StartBtn()
    {
        SceneManager.LoadScene("Level01");
    }


    public void ContinueBtn()
    {
        if(!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().isDead) ToggleWindow(pauseMenu);
    }

    public void QuitBtn()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartMenu")) Application.Quit();
        else SceneManager.LoadScene("StartMenu");
    }

    public void SettingsBtn()
    {
        //print("Settings");
    }

    public void LoadBtn()
    {
        //print("Load");
    }

    public void Dialog()
    {
        ToggleWindow(dialogMenu);
    }

    public void SetDialogText(string dialog)
    {
        dialogText.text = dialog;
        //Text bei Türen ändern
    }

    void ToggleWindow(GameObject window)
    {
        //print("WechsleFenster");
        //print("TimeScale: " + Time.timeScale);
        
        if (window.activeSelf && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().isDead)
        {
            Time.timeScale = 1;
            window.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            window.SetActive(true);
            audioSource.Play();
        }
    }

}
