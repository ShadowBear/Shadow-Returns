using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LibaryController : MonoBehaviour
{
    public int stageCounter = 1;
    public int towerDamage = 10;
    public GameObject ghost,zombie,tower;
    public GameObject boss;
    public GameObject[] chains;
    public GhostRage ghostRageScript;

    private bool gamePaused = false;
    private bool startArena = false;

    public GameObject towerParent;
    [SerializeField]
    private Transform[] towerSP;
    public GameObject slimeParent;
    private Transform[] slimeSP;
    public GameObject otherParent;
    private Transform[] othersSP;

    // Use this for initialization
    void Start()
    {
        towerSP = towerParent.GetComponentsInChildren<Transform>();
        slimeSP = slimeParent.GetComponentsInChildren<Transform>();
        othersSP = otherParent.GetComponentsInChildren<Transform>();
        ghostRageScript.enabled = false;
        SetChains(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startArena) CheckEnemyState();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartArenaFight();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //Part = which four Towers spawn 1-4 First: i = 1, 5-8 Second: i = 5
    IEnumerator InstantiateTowers(int part)
    {
        gamePaused = true;
        yield return new WaitForSeconds(2f);
        for (int i = part; i < part + (towerSP.Length/2); i += 1)
        {
            towerSP[i].position = new Vector3(towerSP[i].position.x, towerSP[i].position.y + 0.85f, towerSP[i].position.z); 
            GameObject towerInstance = Instantiate(tower, towerSP[i].position, towerSP[i].rotation);
            towerInstance.GetComponent<HealthScript>().SetHealth(towerDamage * stageCounter);
        }
        gamePaused = false;
        yield return null;
    }

    IEnumerator InstantiateEnemies(int numbers)
    {
        gamePaused = true;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < numbers; i++)
        {
            //Start with 1 not 0 because 0 is the parent object
            int pos = (int)Random.Range(1, othersSP.Length - 1);

            GameObject enemy = Instantiate(zombie, othersSP[i].position, othersSP[i].rotation);
            //enemy.GetComponent<HealthScript>().SetHealth(towerDamage * stageCounter);
        }
        gamePaused = false;
        yield return null;
    }

    void StartArenaFight()
    {
        startArena = true;
        SetChains(true);
    }

    void EndArenaFight()
    {
        startArena = false;
        SetChains(false);

    }

    void SetChains(bool active)
    {
        foreach (GameObject chain in chains)
        {
            chain.SetActive(active);
        }
    }
    void CheckEnemyState()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Boss") == null && !gamePaused)
        {
            if(stageCounter < 5 && stageCounter > 0) nextStage(stageCounter);
        }
    }

    void nextStage(int stage)
    {
        switch (stage)
        {
            case 1:
                StartCoroutine(InstantiateTowers(1));
                StartCoroutine(InstantiateEnemies(stage * 5));
                slimeParent.GetComponent<SlimeSpawner>().enabled = true;
                stageCounter++;
                print("Round 1");
                break;
            case 2:
                StartCoroutine(InstantiateTowers(5));
                StartCoroutine(InstantiateEnemies(stage * 5));
                boss.SetActive(true);
                stageCounter++;
                break;
            case 3:
                ghostRageScript.enabled = true;
                StartCoroutine(InstantiateEnemies(stage * 5));
                stageCounter++;
                break;
            case 4:
                slimeParent.GetComponent<SlimeSpawner>().enabled = false;
                StartCoroutine(Victory());
                break;
            default:
                StartCoroutine(InstantiateTowers(0));
                break;
        }
    }

    IEnumerator Victory()
    {
        ghostRageScript.enabled = false;
        slimeParent.GetComponent<SlimeSpawner>().enabled = false;
        EndArenaFight();
        yield return null;
    }

}
