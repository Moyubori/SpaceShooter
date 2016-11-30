using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {
	public float scrollRate;

	private float _scrollRate;
	//currently only holding value

	void Awake(){
		_scrollRate = scrollRate;
	}

	void Update (){
		scrollRate = _scrollRate * Time.timeScale;
	}

}
