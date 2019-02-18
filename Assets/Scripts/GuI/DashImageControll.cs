using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashImageControll : MonoBehaviour {

    public Image dash;
    private bool canDash = false;
    public float dashReloadTime = 5f;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!canDash)
        {
            dash.fillAmount += Time.deltaTime / dashReloadTime;
            if (dash.fillAmount >= 1) canDash = true;
        }
	}

    public bool CanDash()
    {
        return canDash;
    }

    public void Dashing()
    {
        canDash = false;
        dash.fillAmount = 0;
    }

}
