using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndroidRotation : MonoBehaviour
{
    public FireJoystick fireJS;
    public bool onDrag = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onDrag)
        {
            // Fehler um 180 Grad
            float angle = (Mathf.Atan2(fireJS.Horizontal(), fireJS.Vertical()) * Mathf.Rad2Deg) - 180;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }
    }
}
