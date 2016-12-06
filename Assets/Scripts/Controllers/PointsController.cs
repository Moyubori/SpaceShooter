using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsController : MonoBehaviour {

	private ScoreClass scoreObject;
	private int points = 0;
	private Text text;

	void Start(){
		text = GetComponent<Text> ();
		scoreObject = Resources.FindObjectsOfTypeAll<ScoreClass> () [0];
	}

	void Update(){
		if (points != scoreObject.totalScore) {
			text.text = scoreObject.totalScore.ToString();
		}
	}

}
