using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {

    private bool AntiPrell = false;
    public int NextSceneToLoad;
    public GameObject loadingScreen;
    public Slider progressBar;

    public Text loadingProgressText;
    private AsyncOperation operation;

    private IEnumerator loadingEnumerator;

    private void Update()
    {
        if (loadingEnumerator != null)
        {
            if (loadingEnumerator.MoveNext())
            {
                if (operation.progress < 0.9f)
                {
                    progressBar.value = operation.progress;
                    loadingProgressText.text = (operation.progress * 100).ToString("F0") + " %";
                }
            }
        }
    }

    IEnumerator AsynchronLoadScene(int sceneIndex)
    {
        Time.timeScale = 0;
        loadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;


        while (operation.progress < 0.9f)
        {
            yield return null;
        }
        operation.allowSceneActivation = true;
        while (operation.progress < 1.0f)
        {
            yield return null;
        }

        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetButtonDown("Action") && Time.timeScale > 0 && !AntiPrell)
        {
#elif UNITY_ANDROID
            if ((AndroidActionButton.androidActionButton.clicked ) && Time.timeScale > 0 && !AntiPrell)
            {
#endif
            AntiPrell = true;
            //Debug.Log("ChangeScene");
            loadingEnumerator = AsynchronLoadScene(NextSceneToLoad);
            progressBar.value = 0;
        }
    }


}
