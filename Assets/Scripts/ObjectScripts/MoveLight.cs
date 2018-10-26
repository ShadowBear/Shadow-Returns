using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour {

    //private GameObject moveObject;
    public Transform[] movePoints;
    private Vector3 nextPosition;
    public float speed = 0.5f;


    // Use this for initialization
	void Start () {
        //moveObject = gameObject;
        nextPosition = movePoints[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs((transform.position - nextPosition).magnitude) < 0.4f)
        {
            int nextPos = (int)Random.Range(0, movePoints.Length);
            setNextPosition(nextPos);
        }
        else
        {
            transform.position += (nextPosition - transform.position) * Time.deltaTime * speed;
        }
	}

    private void setNextPosition(int pos)
    {
        nextPosition = movePoints[pos].transform.position; 
    }

}
