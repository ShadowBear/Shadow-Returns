using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour {

    public static QuestLog questLog;
    public List<Quest> activeQuests = new List<Quest>();
    private int maxActiveQuests = 9;
    private string questLogString;
    public Text questlogText;
    public GameObject questLogWindow;

    // Use this for initialization
    void Start () {
        if (questLog == null) questLog = this;
        else if (questLog != this) Destroy(gameObject);
        questLogString = "";
        UpdateQuestLogText();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L)) ToggleWindow(questLogWindow);
        
	}

    public void AddQuest(Quest q)
    {
        if (activeQuests.Count >= maxActiveQuests) { } //Debug.Log("Questlog Voll");
        else
        {
            //Debug.Log("Quest added");
            activeQuests.Add(q);
            //Update Questlog Text
            UpdateQuestLogText();
        }
    }

    public void RemoveQuest(Quest q)
    {
        //Debug.Log("Quest removed");
        activeQuests.Remove(q);
        UpdateQuestLogText();
    }


    public void CloseQuestlog()
    {
        //Close Questlog
        ToggleWindow(questLogWindow);
    }

    private void UpdateQuestLogText()
    {
        questLogString = "";
        int questCount = 1;
        foreach(Quest q in activeQuests)
        {
            questLogString += questCount + ". " + q.QuestGiverName + " : "+ q.Description + "\n";
            questCount++;
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

    public void ItemCollected(string itemID)
    {
        foreach (Quest q in activeQuests)
        {
            foreach (Goal g in q.Goals)
            {
                g.ItemCollected(itemID);
            }
        }
    }

    public void EnemyDied(int enemyID)
    {
        foreach (Quest q in activeQuests)
        {
            foreach (Goal g in q.Goals)
            {
                g.EnemyDied(enemyID);
            }
        }
    }

    public void DialogDone(string itemID, bool done)
    {
        foreach (Quest q in activeQuests)
        {
            foreach (Goal g in q.Goals)
            {
                g.SpokenTo(itemID,done);
            }
        }
    }

    public void QuestCompleted(string questID)
    {
        foreach (Quest q in activeQuests)
        {
            foreach (Goal g in q.Goals)
            {
                g.QuestGoalCompleted(questID);
            }
        }
    }

}
