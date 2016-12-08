using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PointsCounter : MonoBehaviour {

	private Text pointsCounter;

	void Start(){
		pointsCounter = GetComponent<Text> ();
	}

	void Update(){
		pointsCounter.text = GameData.gameData.score.ToString();
	}

}
