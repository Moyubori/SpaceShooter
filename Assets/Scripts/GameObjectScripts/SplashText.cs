using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SplashText : MonoBehaviour {

	private Text splashText;

	void Start(){
		splashText = GetComponent<Text> ();

		if (GameData.gameData.winLevel) {
			splashText.text = "Level Complete";
		} else {
			splashText.text = "Game Over";
		}
	}


}
