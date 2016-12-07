using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PointsController : MonoBehaviour {

	private ScoreClass scoreObject;
	private int points = 0;
	private Text pointsCounter;

	void Start(){
		pointsCounter = GetComponent<Text> ();
		try{
			scoreObject = Resources.FindObjectsOfTypeAll<ScoreClass> () [0];
		} catch(IndexOutOfRangeException e){
			scoreObject = Resources.Load ("score") as ScoreClass;
		}
	}

	void Update(){
		if (points != scoreObject.totalScore) {
			pointsCounter.text = scoreObject.totalScore.ToString();
		}
	}

}
