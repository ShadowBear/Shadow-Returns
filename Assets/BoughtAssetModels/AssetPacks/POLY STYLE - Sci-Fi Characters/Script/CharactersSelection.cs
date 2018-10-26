using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSelection : MonoBehaviour {

	public GameObject character1;
	public GameObject character2;
	public GameObject character3;
	public GameObject character4;
	public GameObject character5;
	public GameObject character6;


	void setCharacter01 (){
		onActiveFalse ();
		character1.SetActive(true);
	}
	void setCharacter02 (){
		onActiveFalse ();
		character2.SetActive(true);
    }
	void setCharacter03 (){
		onActiveFalse ();
		character3.SetActive(true);
    }
	void setCharacter04 (){
		onActiveFalse ();
		character4.SetActive(true);
    }
	void setCharacter05 (){
		onActiveFalse ();
		character5.SetActive(true);
    }
	void setCharacter06 (){
		onActiveFalse ();
		character6.SetActive(true);
    }

	void onActiveFalse()
	{
		character1.SetActive(false);
        character2.SetActive(false);
        character3.SetActive(false);
        character4.SetActive(false);
        character5.SetActive(false);
        character6.SetActive(false);

    }
}
