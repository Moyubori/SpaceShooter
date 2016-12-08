using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealthbar : MonoBehaviour {

	private Text percentage;
	private EnemyController boss;

	void Start(){
		percentage = GameObject.FindGameObjectWithTag ("BossHPPercentage").GetComponent<Text> ();
		boss = transform.parent.GetComponent<EnemyController> ();

		transform.parent = GameObject.FindGameObjectWithTag ("MainHUD").transform;
		GetComponent<RectTransform> ().anchoredPosition = transform.parent.GetComponent<RectTransform> ().anchoredPosition;
		GetComponent<RectTransform> ().localScale = Vector3.one;

	}

	void Update(){
		if (boss.gameObject.activeSelf) {
			float calculatedPercentage = (float)boss.health / boss.maxHealth * 100;
			percentage.text = calculatedPercentage.ToString ();
		} else {
			percentage.text = "0";
		}
	}

}
