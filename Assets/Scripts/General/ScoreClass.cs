using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Score", menuName = "")]
public class ScoreClass : ScriptableObject {

	public int totalScore;

	// add point modifiers/combos/anything later
	public void AddPoints(int points){
		totalScore += points;
	}

	void Awake(){
		totalScore = 0;
	}

	public void Reset(){
		totalScore = 0;
	}
}