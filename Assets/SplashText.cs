using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SplashText : MonoBehaviour {

	private ScoreClass scoreObject;
	private Text splashText;

	void Start(){
		try{
			scoreObject = Resources.FindObjectsOfTypeAll<ScoreClass>()[0];
		}  catch(IndexOutOfRangeException e){
			scoreObject = Resources.Load ("score") as ScoreClass;
		}
		splashText = GetComponent<Text> ();

		if (scoreObject.win) {
			splashText.text = "Level Complete";
		} else {
			splashText.text = "Game Over";
		}
	}


}
