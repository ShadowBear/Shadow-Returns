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
    private int maxPotionsNmbr = 6;
    public Text potionText;
    public int potionValue = 65;

    public int keyNmbr;
    public Text keyText;

    private int coinAmount;
    public Text coinText;

    public Text ammuText;

    //Display Damage on Screen
    public Canvas canvas;
    public Text textTmp;
    public GameObject damageTextObject;
    private float scaleRate = 1;

    //Sword, Shield and Pistol 
    //public bool swordCollected = false;
    //public bool gunCollected = false;
    public bool shieldCollected = false;

    public static GameManager control;    
    
    // Use this for initialization
	void Awake () {
        //Unique GameManager
        DontDestroyOnLoad(gameObject);
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
        //swordCollected = true;
        //gunCollected = true;
        shieldCollected = true;
    }

    public void ShowDmgText(float damage, Transform displayTrans)
    {
        ///******Test old*******///
        //Component dmgText = Instantiate(textTmp, displayTrans.position, Quaternion.identity) as Component;
        //dmgText.transform.SetParent(canvas.transform);
        //dmgText.GetComponent<Text>().text = ((int)damage).ToString();
        //dmgText.transform.localScale = new Vector3(scaleRate, scaleRate, scaleRate);
        //if (damage > 15) dmgText.GetComponent<Text>().color = Color.red;
        //dmgText.gameObject.SetActive(true);

        //dmgText.transform.GetComponent<Text>().transform.position = displayTrans.position + new Vector3(0,1,0);
        //dmgText.transform.GetComponent<Text>().transform.position = Camera.main.WorldToScreenPoint(displayTrans.position);

        ////********* NEW ***********///

        GameObject dmgText = Instantiate(damageTextObject);
        if (!GameObject.FindGameObjectWithTag("Canvas")) Debug.Log("Canvas nicht gefunden");
        dmgText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        dmgText.GetComponentInChildren<Text>().text = ((int)damage).ToString();
        dmgText.transform.localScale = new Vector3(scaleRate, scaleRate, 1);
        if (damage > 25) dmgText.GetComponentInChildren<Text>().color = Color.red;
        dmgText.gameObject.SetActive(true);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(displayTrans.position);
        dmgText.transform.position = screenPosition + new Vector2(0, 1.5f);
        //dmgText.transform.position = displayTrans.position + new Vector3(0,1.5f,0);

    }

    public void ReceiveExperience(int experience)
    {
        experiencePoints += experience;
        if(experiencePoints >= nextLvlExperience)
        {
            playerLvl++;
            experiencePoints -= nextLvlExperience;
            nextLvlExperience = (int)(nextLvlExperience * lvlScale);
            lvlText.text = playerLvl.ToString();
            //if u get More Exp than the next level also needed do it again
            if (experiencePoints >= nextLvlExperience) ReceiveExperience(0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().LevelUpHealth();
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

    //public void SwordCollect()
    //{
    //    swordCollected = true;
    //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().AddWeapon("sword");
    //}

    //public void GunCollect()
    //{
    //    gunCollected = true;
    //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().AddWeapon("gun");
    //}

    //public void ShieldCollect()
    //{
    //    shieldCollected = true;
    //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().AddWeapon("shield");
    //}

}
