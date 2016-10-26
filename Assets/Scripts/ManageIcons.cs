using UnityEngine;
using System.Collections;

public class ManageIcons : MonoBehaviour {

	private Transform[] icons;

	void Start () {
		int iconQuantity = transform.childCount;
		icons = new Transform[iconQuantity];

		for (int i = 0; i < iconQuantity; i++) {
			icons [i] = transform.GetChild (i);
		}
	}

	public void SetHealth(int health){
		for (int i = 0; i < health; i++) {
			icons [i].gameObject.SetActive (true);
		}
		for (int i = health; i < 5; i++) {
			icons [i].gameObject.SetActive (false);
		}
	}
}
