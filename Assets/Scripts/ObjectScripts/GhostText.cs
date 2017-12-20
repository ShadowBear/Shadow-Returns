using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostText : MonoBehaviour {

    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) canvas.SetActive(true);

    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player")) canvas.SetActive(false);

    }


}
