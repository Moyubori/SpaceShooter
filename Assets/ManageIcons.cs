using UnityEngine;
using System.Collections;

public class ManageIcons : MonoBehaviour {

	private int iconQuantity = 5;

	private Transform[] icons;


	public void SetHealth(int health){
		for (int i = 0; i < health; i++) {
			icons [i].gameObject.active = true;
		}
		for (int i = health; i < 5; i++) {
			icons [i].gameObject.active = false;
		}
	}


	void Start () {
		icons = new Transform[iconQuantity];

		for (int i = 0; i < iconQuantity; i++) {
			icons [i] = transform.GetChild (i);
		}
	}
}
