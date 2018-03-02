using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControll : MonoBehaviour {

    // Use this for initialization
    public Transform playerRotationTrans;
	
	// Update is called once per frame
	void Update () {
        transform.position = Input.mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, -playerRotationTrans.rotation.eulerAngles.y);

        //Plane plane = new Plane(Camera.main.transform.forward, playerRotationTrans.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ////var dist : float;
        //float dist;
        //if (plane.Raycast(ray, out dist))
        //{
        //    Vector3 v3Pos = ray.GetPoint(dist);
        //    v3Pos.y = 0;

        //    v3Pos = Camera.main.ViewportToScreenPoint(v3Pos);
        //    transform.position = v3Pos;
        //    transform.rotation = Quaternion.Euler(0, 0, -playerRotationTrans.rotation.eulerAngles.y);
        //}

    }
}
