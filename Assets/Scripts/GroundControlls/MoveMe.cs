using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {


    public Vector3 positionUp;
    public Vector3 positionDown;

    public bool moveUp = false;
    public bool moveDown = false;

    [SerializeField]
    private float movingSpeed = 0.075f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moveDown && !moveUp) MoveDown();
        else if (moveUp && !moveDown) MoveUp();
	}

    protected void MoveUp()
    {
        if (transform.position.y < positionUp.y)
        {
            transform.position += new Vector3(0, movingSpeed, 0);
        }
        else moveUp = false;
    }

    protected void MoveDown()
    {
        if (transform.position.y > positionDown.y)
        {
            transform.position += new Vector3(0, -movingSpeed, 0);
        }
        else moveDown = false;
        
    }

}

