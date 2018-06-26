using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardBoss : MonoBehaviour {

    public int lifePieces;
    public GraveyardController gyControll;
    public GameObject canvas;
    private Quaternion canvasRotation;
    public GameObject explosion;
    public float attackRate = 4.5f;
    public float radius;
    public float chargeSpeed;

    private bool canAttack = true;
    private bool active = false;
    private bool charging = false;
    private bool dying = false;
    private GameObject player;
    private Rigidbody rigid;
    private Animator anim;


    // Use this for initialization
    void Start () {
        lifePieces = gyControll.GetGhostPieces();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        canvasRotation = canvas.transform.rotation;
        StartCoroutine(Talk());
	}
	
	// Update is called once per frame
	void Update () {
		if(active && canAttack && !dying)
        {
            StartCoroutine(Attack());
        }
        if (charging && !dying)
        {
            rigid.AddForce(transform.forward * chargeSpeed, ForceMode.Force);
        }
	}

    IEnumerator Talk()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(3);
        canvas.SetActive(false);
        transform.LookAt(player.transform);
        charging = true;
        yield return new WaitForSeconds(1f);
        charging = false;
        SetGhost(false);
        yield return new WaitForSeconds(attackRate);
        active = true;
        yield return null;
    }

    IEnumerator Attack()
    {
        canAttack = false;
        // get a random direction (360°) in radians
        float angle = Random.Range(0.0f, Mathf.PI * 2);
        // create a vector with length 1.0
        Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        // scale it to the desired length
        transform.position = player.transform.position + V * radius;
        transform.LookAt(player.transform);

        // Set Ghost active and begin chargeAttack
        SetGhost(true);
        charging = true;
        yield return new WaitForSeconds(1.5f);
        charging = false;
        SetGhost(false);
        
        //wait for the next attack 2 start
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
        yield return null;
    }

    private void SetGhost(bool state)
    {
        foreach(Transform child in transform)
        {
            if(child != canvas.transform) child.gameObject.SetActive(state);
        }
    }

    public void Die()
    {
        //print("Sterbe");
        StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        //Instantiate(explosion, transform.position, explosion.transform.rotation);
        dying = true;
        transform.position = player.transform.position + player.transform.forward * 2;
        transform.LookAt(player.transform);
        canvas.SetActive(true);
        canvas.transform.rotation = canvasRotation;
        canvas.GetComponentInChildren<Text>().text = "Äaahh You Won!\nBut others will come";
        SetGhost(true);
        if (anim != null) anim.SetBool("die", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    }
}
