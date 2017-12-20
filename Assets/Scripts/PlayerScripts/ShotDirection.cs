using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDirection : MonoBehaviour {

    public GameObject rotatedObject;
    
    // Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = -AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        angle -= 90;

        //Ta Daaa
        rotatedObject.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
