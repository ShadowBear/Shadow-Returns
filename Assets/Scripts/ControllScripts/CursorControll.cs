using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControll : MonoBehaviour {

    // Use this for initialization
    public Transform playerRotationTrans;

    private void Start()
    {
        playerRotationTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update () {
        transform.position = Input.mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, -playerRotationTrans.rotation.eulerAngles.y);
    }
}
