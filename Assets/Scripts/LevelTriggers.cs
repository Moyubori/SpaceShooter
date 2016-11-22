using UnityEngine;
using System.Collections;

public class LevelTriggers : MonoBehaviour {

	public Camera mainCam;

	void Update () {
		// moves the camera right
		// only a temporary solution
		//mainCam.transform.position = new Vector3(mainCam.transform.position.x + 1f, mainCam.transform.position.y, mainCam.transform.position.z);
	}
}
