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
		RectTransform rect = GetComponent<RectTransform> ();
		rect.sizeDelta = transform.parent.GetComponent<RectTransform> ().sizeDelta;
		rect.localScale = Vector3.one;
		rect.anchoredPosition = new Vector2(rect.sizeDelta.x/2,rect.sizeDelta.y/2);

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
