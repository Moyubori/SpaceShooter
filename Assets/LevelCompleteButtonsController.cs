using UnityEngine;
using System.Collections;

public class LevelCompleteButtonsController : MonoBehaviour {

	public void Play(){
		Application.LoadLevel ("shmup");
	}

	public void ReturnToMenu(){
		Application.LoadLevel ("main_menu");
	}
}
