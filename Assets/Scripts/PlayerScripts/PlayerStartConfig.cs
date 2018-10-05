using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartConfig : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        Destroy(gameObject);
    }
}
