using UnityEngine;
using System.Collections;

public class ManageIcons : MonoBehaviour {

	private Transform[] icons = new Transform[5];

	public void SetHealth(int health){
		for (int i = 0; i < health; i++) {
			icons [i].gameObject.active = true;
		}
		for (int i = health; i < 5; i++) {
			icons [i].gameObject.active = false;
		}
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; i++) {
			icons [i] = transform.GetChild (i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
