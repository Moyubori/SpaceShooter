using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	public Camera camera;
	public float scrollRate;

	private void Update() {
		Translate ();
		CheckIfOutOfCamera ();
	}

	private void Translate() {
		Vector2 position = transform.position;
		position.x -= scrollRate;
		transform.position = position;
	}

	private void CheckIfOutOfCamera() {
		float cameraBoundary = camera.orthographicSize * camera.aspect;
		float width = GetComponent<SpriteRenderer> ().bounds.size.x;
		float minPosition = width / 2 + cameraBoundary;

		if (transform.position.x < -minPosition) {
			gameObject.SetActive (false);
		}
	}
}