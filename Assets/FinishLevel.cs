using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishLevel : Event {

	public RectTransform levelComplete;

	override protected IEnumerator EventActions (){
		iTween.ValueTo (levelComplete.gameObject, iTween.Hash (
			"from", levelComplete.anchoredPosition,
			"to", new Vector2(levelComplete.anchoredPosition.x, 100),
			"time", 2.0f,
			"easeType", "easeInOutElastic",
			"onupdatetarget", this.gameObject,
			"onupdate", "SlideText"
		));
			
		yield return new WaitForSeconds (5.0f);
		Application.LoadLevel ("level_complete");

		FinishEvent ();
	}

	public void SlideText(Vector2 newPos){
		levelComplete.anchoredPosition = newPos;
	}

}
