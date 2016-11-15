using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthbarManager : MonoBehaviour {

	[SerializeField]
	private Image fillBar;

	public PlayerController player;

	void Awake(){
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		if (player == null) {
			Debug.Log ("Player object not found");
		}
	}

	// health should be a number between 0 and 1
	public void SetHealth(int health){
		fillBar.fillAmount = ((float)health/player.maxHealth);
		//Debug.Log (greenBar.fillAmount);
	}
}
