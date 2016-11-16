using UnityEngine;
using System.Collections;

public class Projectile : InflictingDamage {

	public float speed = 10f;
	//public int damage = 10;

	[SerializeField]
	private Renderer renderer;

	void CheckIfOutOfCamera(){
//		//need to calculate boundaries here because the camera might change its size
//		float yBoundary = Camera.main.orthographicSize;
//		float xBoundary = yBoundary * Camera.main.aspect;
//		Debug.Log("xy bounds: " + xBoundary + " " + yBoundary);
//
//
//		if (transform.localPosition.x > xBoundary || transform.localPosition.x < -xBoundary || transform.localPosition.y > yBoundary || transform.localPosition.y < -yBoundary) {
//			gameObject.SetActive (false);
//		}

		// that is good enough, right?
		if (!renderer.isVisible) {
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

	void Update () {
		// movement of the projectile
		transform.localPosition += transform.right * speed * Time.deltaTime;
	}
}
