using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    public float lifeTime;
    private float timePassed;

    // Use this for initialization
    void Start()
    {
        timePassed = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed -= Time.deltaTime;
        if (timePassed <= 0) Destroy(gameObject);
    }
}
