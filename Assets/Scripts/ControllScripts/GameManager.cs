using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int playerHealth = 100;
    public int shieldHealth = 100;

    //Display Damage on Screen
    public Canvas canvas;
    public Text textTmp;
    private float scaleRate = 0.005f;

    public static GameManager control;
    
    
    // Use this for initialization
	void Awake () {
        //Unique GameManager
        if (control == null) control = this;
        else if (control != this) Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowDmgText(float damage, Transform displayTrans)
    {
        Component dmgText = MonoBehaviour.Instantiate(textTmp, displayTrans.position, Quaternion.identity) as Component;
        dmgText.transform.SetParent(canvas.transform);
        dmgText.GetComponent<Text>().text = ((int)damage).ToString();
        dmgText.transform.localScale = new Vector3(scaleRate, scaleRate, scaleRate);
        if (damage > 15) dmgText.GetComponent<Text>().color = Color.red;
        dmgText.gameObject.SetActive(true);
        dmgText.transform.GetComponent<Text>().transform.position = displayTrans.position + new Vector3(0,1,0);
        //dmgText.transform.GetComponent<Text>().transform.position = Camera.main.WorldToScreenPoint(displayTrans.position);
    }

}
