using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour {

    private List<Quest> activeQuests = new List<Quest>();
    private int maxActiveQuests = 9;
    private string questLogString;
    public Text questlogText;
    public GameObject questLog;

    // Use this for initialization
    void Start () {
        questLogString = "";
        UpdateQuestLogText();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L)) ToggleWindow(questLog);
        
	}

    public void AddQuest(Quest q)
    {
        if (activeQuests.Count >= maxActiveQuests) Debug.Log("Questlog Voll");
        else
        {
            Debug.Log("Quest added");
            activeQuests.Add(q);
            //Update Questlog Text
            UpdateQuestLogText();
        }
    }

    public void RemoveQuest(Quest q)
    {
        Debug.Log("Quest removed");
        activeQuests.Remove(q);
        UpdateQuestLogText();
    }


    public void CloseQuestlog()
    {
        //Close Questlog
        ToggleWindow(questLog);
    }

    private void UpdateQuestLogText()
    {
        questLogString = "";
        foreach(Quest q in activeQuests)
        {
            questLogString += q.Description + "\n";
        }
        //Delete the last return
        if (questLogString.Length > 2) questLogString = questLogString.Substring(0, questLogString.Length - 2);
        else questLogString = "No ActiveQuests availabe!";
        questlogText.text = questLogString;
    }

    void ToggleWindow(GameObject window)
    {
        if (window.activeSelf && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().isDead)
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
