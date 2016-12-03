using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

	GameObject[] menuObjects;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				ShowPaused ();
			} else {
				Time.timeScale = 1;
				HidePaused ();
			}
		}
	}

	void Start () {
		Time.timeScale = 1;
		menuObjects = GameObject.FindGameObjectsWithTag ("PauseMenu");
		HidePaused ();
	}

	public void Resume(){
		Time.timeScale = 1;
		HidePaused ();
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Quit(){
		Application.Quit ();
	}

	private void HidePaused(){
		foreach(GameObject i in menuObjects){
			i.SetActive (false);
		}
	}

	private void ShowPaused(){
		foreach(GameObject i in menuObjects){
			i.SetActive (true);
		}
	}
}
