using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int playerHealth = 100;
    public int shieldHealth = 100;

    //Experience Values
    private int experiencePoints = 0;
    [SerializeField]
    private int playerLvl = 1;
    public Text lvlText;
    [SerializeField]
    private int nextLvlExperience = 200;
    private float lvlScale = 1.2f;

    public Image experienceImage;
    public Text experienceText;

    //Dash Images and Value
    public GameObject dashBlocksParent;

    //Potions, Keys and Coins
    public int potionNmbr;
    private int maxPotionsNmbr = 10;
    public Text potionText;
    public int potionValue = 40;

    public int keyNmbr;
    public Text keyText;

    private int coinAmount;
    public Text coinText;

    public Text ammuText;

    //Display Damage on Screen
    public Canvas canvas;
    public Text textTmp;
    private float scaleRate = 0.005f;

    //Sword, Shield and Pistol 
    public bool swordCollected = false;
    public bool gunCollected = false;
    public bool shieldCollected = false;

    public static GameManager control;    
    
    // Use this for initialization
	void Awake () {
        //Unique GameManager
        if (control == null) control = this;
        else if (control != this) Destroy(gameObject);
	}

    private void Start()
    {
        experienceText.text = experiencePoints + "/" + nextLvlExperience;
        experienceImage.fillAmount = (float)experiencePoints / nextLvlExperience;
    }

    public void SuitUp()
    {
        swordCollected = true;
        gunCollected = true;
        shieldCollected = true;
    }

    public void ShowDmgText(float damage, Transform displayTrans)
    {
        Component dmgText = Instantiate(textTmp, displayTrans.position, Quaternion.identity) as Component;
        dmgText.transform.SetParent(canvas.transform);
        dmgText.GetComponent<Text>().text = ((int)damage).ToString();
        dmgText.transform.localScale = new Vector3(scaleRate, scaleRate, scaleRate);
        if (damage > 15) dmgText.GetComponent<Text>().color = Color.red;
        dmgText.gameObject.SetActive(true);
        
        dmgText.transform.GetComponent<Text>().transform.position = displayTrans.position + new Vector3(0,1,0);
        //dmgText.transform.GetComponent<Text>().transform.position = Camera.main.WorldToScreenPoint(displayTrans.position);
    }

    public void ReceiveExperience(int experience)
    {
        experiencePoints += experience;
        if(experiencePoints > nextLvlExperience)
        {
            playerLvl++;
            experiencePoints -= nextLvlExperience;
            nextLvlExperience = (int)(nextLvlExperience * lvlScale);
            lvlText.text = playerLvl.ToString();
        }
        experienceText.text = experiencePoints + "/" + nextLvlExperience;
        experienceImage.fillAmount = (float)experiencePoints / nextLvlExperience;

    }

    public void CollectPotion()
    {
        potionNmbr++;
        if (potionNmbr > maxPotionsNmbr) potionNmbr = maxPotionsNmbr;
        potionText.text = potionNmbr.ToString();
    }

    public void DrinkPotion()
    {
        potionNmbr--;
        if (potionNmbr < 0) potionNmbr = 0;
        potionText.text = potionNmbr.ToString();
    }

    public void CollectKey()
    {
        keyNmbr++;
        keyText.text = keyNmbr.ToString();
    }

    public void UseKey()
    {
        keyNmbr--;
        keyText.text = keyNmbr.ToString();
    }

    public void CollectCoins(int value)
    {
        coinAmount += value;
        if (coinAmount > 9999) coinAmount = 9999;
        coinText.text = coinAmount.ToString();
    }

    public void SwordCollect()
    {
        swordCollected = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().AddWeapon("sword");
    }

    public void GunCollect()
    {
        gunCollected = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().AddWeapon("gun");
    }

    public void ShieldCollect()
    {
        shieldCollected = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().AddWeapon("shield");
    }

}
