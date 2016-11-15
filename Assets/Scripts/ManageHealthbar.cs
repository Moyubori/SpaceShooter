using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageHealthbar : MonoBehaviour {

	[SerializeField]
	private Image greenBar;

	public PlayerController player;

	void Start(){
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		if (player == null) {
			Debug.Log ("dupa");
		}
	}

	// health should be a number between 0 and 1
	public void SetHealth(int health){
		greenBar.fillAmount = ((float)health/player.maxHealth);
		//Debug.Log (greenBar.fillAmount);
	}
}
