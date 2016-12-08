using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

	public static GameData gameData; 

	[SerializeField]
	private int _score = 0;
	public int score{
		get { return _score; }
		set { AddPoints (value); }
	}

	public bool winLevel = false;

	// add point modifiers/combos/anything later
	void AddPoints(int points){
		_score += points;
	}

	void Awake () {
		// this is a singleton class for transfering data
		// if there is another instance of GameData, this one will destroy itself
		if (gameData == null) {
			// the object will persist through scene loads
			DontDestroyOnLoad (gameObject);
			gameData = this;
		} else if(gameData != this){
			Destroy (gameObject);
		}
	}

	public void Reset(){
		_score = 0;
		winLevel = false;
	}

}
