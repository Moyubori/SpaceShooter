using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public Camera mainCam;
	public Transform gameField;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameField.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0);
	}
}
