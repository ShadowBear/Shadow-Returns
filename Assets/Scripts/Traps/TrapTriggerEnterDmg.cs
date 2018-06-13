using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggerEnterDmg : MonoBehaviour
{

    public int trapDMG = 35;

    public void SetTrapDmg(int dmg)
    {
        trapDMG = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthScript>()) other.GetComponent<HealthScript>().TakeDamage(trapDMG);
    }
}