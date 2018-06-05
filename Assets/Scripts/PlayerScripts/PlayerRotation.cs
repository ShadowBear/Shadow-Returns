using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    private PlayerAttack playerAttack;
    private Vector2 mousePos;
    private Vector2 screenPos;

    private Vector3 cursorPos = Vector3.zero;

    public LayerMask floorMask;

 
    // Use this for initialization
    void Start () {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (playerAttack.isAttacking && !playerAttack.rangeAttack) { }
        else Turning();
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        

        if(Physics.Raycast(camRay, out floorHit,100 ,floorMask))
        {
            Debug.DrawRay(camRay.direction, floorHit.point);
            cursorPos = floorHit.point;
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;            
        }       
      
    }

    //Maybefor Later Use
    public Vector3 GetCursorPos()
    {
        return cursorPos;
    }

    public int GetDirection()
    {
        // Animation Direction:
        // Forward = 0
        // Right = 1
        // Left = 2
        // Backward = -1
        Vector3 localRotate = transform.InverseTransformDirection(Camera.main.transform.forward);
        float movementAngle = Mathf.Atan(localRotate.z / localRotate.z);

        if(Mathf.Abs(localRotate.z) > Mathf.Abs(localRotate.x))
        {
            //Forward & Backward
            if (localRotate.z > 0.1f) return -1;
            else if (localRotate.z < -0.1f) return 0;
        }
        else
        {
            //Left & Right
            if (localRotate.x > 0f) return 2;
            else if (localRotate.x < 0f) return 1;
        }       
        return 0;
    }

}
