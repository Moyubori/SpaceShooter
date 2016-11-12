using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float speed = 0.7f;

	private Vector3 sPoint;
	private Vector3 fPoint;
	private float sTime;

	void OnEnable(){
		Start();
	}

	void OnDisable(){
		transform.localPosition = sPoint;
	}

	void Start(){
		sPoint = transform.localPosition;
		fPoint = sPoint;
		fPoint.x -= 5;
		sTime = Time.time;
	}

	void Update () {
		transform.localPosition = Vector3.Lerp (sPoint, fPoint, (Time.time - sTime) * speed);
	}
}
