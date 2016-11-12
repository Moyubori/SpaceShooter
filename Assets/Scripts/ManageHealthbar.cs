using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageHealthbar : MonoBehaviour {

	[SerializeField]
	private Image greenBar;

	// health should be a number between 0 and 1
	public void SetHealth(int health){
		greenBar.fillAmount = (health/100f);
	}
}
