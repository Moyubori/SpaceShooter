﻿using UnityEngine;
using System.Collections;

public class LevelTriggers : MonoBehaviour {

	public Camera mainCam;
	
	void Update () {
		mainCam.transform.position = new Vector3(mainCam.transform.position.x + 0.2f, mainCam.transform.position.y, mainCam.transform.position.z);
	}
}