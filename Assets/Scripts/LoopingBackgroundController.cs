using UnityEngine;
using System.Collections;

public class LoopingBackgroundController : BackgroundController {
	override protected void UpdateChildren (float cameraOffset) {
		foreach (Transform child in transform) {
			if (child.gameObject.activeSelf) {
				UpdateParallax (child, cameraOffset);
				LoopLayer (child);
			}
		}
	}

	//TODO it's harder to do when gamefield moves, do it later
	private void LoopLayer (Transform layer) {
		float cameraLeft = camera.transform.position.x - camera.orthographicSize * camera.aspect;
//		Debug.Log ("camPos: " + camera.transform.position + "\tcamLeft: " + cameraLeft);
		foreach(Transform child in layer) {
			SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer> ();
			float spriteRight = spriteRenderer.bounds.center.x + spriteRenderer.bounds.extents.x;
//			Debug.Log ("SpriteRight: "+spriteRight);
			if (spriteRight < cameraLeft) {
				child.Translate (new Vector3(spriteRenderer.bounds.size.x * 2, 0));
			}
		}
	}
}
