using UnityEngine;
using System.Collections;

public class StartLevel : LevelEvent {

	override protected IEnumerator EventActions(){
		yield return new WaitForSeconds (1f);
		Debug.LogWarning ("3...");
		yield return new WaitForSeconds (1f);
		Debug.LogWarning ("2...");
		yield return new WaitForSeconds (1f);
		Debug.LogWarning ("1...");
		yield return new WaitForSeconds (1f);
		FinishEvent ();
	}

}
