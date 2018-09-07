using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHitbox : MonoBehaviour {

    private GolemEnemy golemEnemy;

    private void Start()
    {
        golemEnemy = GetComponentInParent<GolemEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            golemEnemy.DoMeeleDamage(other);
        }
    }
}
