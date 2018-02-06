using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    private bool swap;
    public Transform nextPos;
    GameObject player;
    float distanceToPlayer;
    private Coroutine swapping;
    // Use this for initialization
    void Start () {
        swap = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (swap) swapping = StartCoroutine(SwapGhost());
        distanceToPlayer = Mathf.Abs((transform.position - player.transform.position).magnitude);
        if (distanceToPlayer < 2)
        {
            StopCoroutine(swapping);
            swapping = StartCoroutine(SwapGhost());
        }
    }

    IEnumerator SwapGhost()
    {
        swap = false;
        transform.position = nextPos.position + new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-5f, 5f));
        transform.LookAt(player.transform);
        yield return new WaitForSeconds(Random.Range(1.5f,3.5f));
        swap = true;
        yield return null;
    }
}
