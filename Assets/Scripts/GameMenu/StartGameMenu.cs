using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameMenu : MonoBehaviour {

    public GameObject loadingScreen;
    public GameObject menuScreen;
    public Slider progressBar;

    public Text loadingProgressText;
    private AsyncOperation operation;

    private IEnumerator loadingEnumerator;

    private void Update()
    {
        if(loadingEnumerator != null)
        {
            if (loadingEnumerator.MoveNext())
            {
                if(operation.progress < 0.9f)
                {
                    progressBar.value = operation.progress;
                    loadingProgressText.text = (operation.progress * 100).ToString("F0") + " %";
                }
            }
        }
    }

    public void ButtonStart()
    {
        loadingEnumerator = AsynchronLoadScene(1);
        progressBar.value = 0;
    }


    IEnumerator AsynchronLoadScene(int sceneIndex)
    {
        menuScreen.SetActive(false);
        loadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        

        while (operation.progress < 0.9f)
        {
            yield return null;
        }
        operation.allowSceneActivation = true;
        while(operation.progress < 1.0f)
        {
            yield return null;
        }

        yield return null;
    }
    

    IEnumerator LoadScene(int sceneIndex)
    {
        
        menuScreen.SetActive(false);
        loadingScreen.SetActive(true);

        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone) 
        {
            //float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = operation.progress;
            loadingProgressText.text = (operation.progress * 100).ToString("F0") + " %";

            if (operation.progress >= 0.9f)
            {
                progressBar.value = 1;
                operation.allowSceneActivation = true;
            }

            yield return null;
        } 
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
