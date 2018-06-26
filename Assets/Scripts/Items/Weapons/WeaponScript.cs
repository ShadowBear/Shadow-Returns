using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {


    public enum WeaponType { gun, sword, axe};

    public WeaponType weapon;
    
    public WeaponType GetProperties()
    {
        return weapon;
    }
}
