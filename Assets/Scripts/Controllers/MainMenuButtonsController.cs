using UnityEngine;
using System.Collections;

public class MainMenuButtonsController : MonoBehaviour {

	public void Play(){
		Application.LoadLevel("shmup");
	}

	public void Options(){
		// To be added
	}

	public void Quit(){
		Application.Quit ();
	}
}
