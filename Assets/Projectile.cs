using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 10f;

	private float xBoundary;
	private float yBoundary;

	void CheckIfOutOfCamera(){
		if (transform.position.x > xBoundary || transform.position.x < -xBoundary || transform.position.y > yBoundary || transform.position.y < -yBoundary) {
			Destroy (gameObject);
		}
	}

	void Start () {
		xBoundary = Camera.main.orthographicSize * Screen.width / Screen.height;
		yBoundary = Camera.main.orthographicSize;

		InvokeRepeating ("CheckIfOutOfCamera", 1, 0.5f);
	}
	
	void Update () {
		transform.position += transform.right * speed * Time.deltaTime;


	}
}
