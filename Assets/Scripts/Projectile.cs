using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 10f;

	void CheckIfOutOfCamera(){
		//need to calculate boundaries here because the camera might change its size
		Debug.Log("Camera size check: " + Camera.main.orthographicSize);
		float xBoundary = Camera.main.orthographicSize * Screen.width / Screen.height;
		float yBoundary = Camera.main.orthographicSize;

		if (transform.position.x > xBoundary || transform.position.x < -xBoundary || transform.position.y > yBoundary || transform.position.y < -yBoundary) {
			gameObject.SetActive (false);
		}
	}
		
	void OnEnable() {
		//every half a second checks if the projectile should be deleted
		InvokeRepeating ("CheckIfOutOfCamera", 1, 0.5f);
	}

	void OnDisable() {
		CancelInvoke ();
	}

	void Start() {
		Debug.Log (transform.right);
	}

	void Update () {
		transform.position += transform.right * speed * Time.deltaTime;
	}
}
