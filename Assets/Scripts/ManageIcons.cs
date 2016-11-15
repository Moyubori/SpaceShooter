using UnityEngine;
using System.Collections;

public class ManageIcons : MonoBehaviour {

	public int iconsActive{
		get { return _iconsActive; }
	}
	private int _iconsActive = 5;

	private Transform[] icons;

	void Start () {
		int iconQuantity = transform.childCount;
		icons = new Transform[iconQuantity];

		for (int i = 0; i < iconQuantity; i++) {
			icons [i] = transform.GetChild (i);
		}
	}

	public void SetLives(int lives){
		for (int i = 0; i < lives; i++) {
			icons [i].gameObject.SetActive (true);
		}
		for (int i = lives; i < 5; i++) {
			icons [i].gameObject.SetActive (false);
		}
		_iconsActive = lives;
	}
}
