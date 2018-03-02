using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    public GameObject player;
    private Vector2 mousePos;
    private Vector2 screenPos;

    public LayerMask floorMask;

 
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        Turning();
        
        
        //Plane plane = new Plane(Camera.main.transform.forward, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ////var dist : float;
        //float dist;
        //if (plane.Raycast(ray, out dist))
        //{
        //    Vector3 v3Pos = ray.GetPoint(dist);
        //    float distanceBetween = Vector3.Distance(v3Pos, transform.position);
        //    print("Distance: " + distanceBetween);
        //    v3Pos.y = 0;
        //    transform.LookAt(v3Pos);
        //    Debug.DrawRay(transform.position, v3Pos, Color.red);
        //}





        /*
        float cameraDistance = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        Vector3 inputMouse = new Vector3(Input.mousePosition.x, cameraDistance, Input.mousePosition.y);
        Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(inputMouse), Color.blue);

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = -AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        angle -= 90;

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        */
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
            
        }
    }

}
