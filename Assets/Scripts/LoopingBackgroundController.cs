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
		
	private void LoopLayer (Transform layer) {
		float cameraLeft = camera.transform.position.x - camera.orthographicSize * camera.aspect;
		foreach(Transform child in layer) {
			SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer> ();
			float spriteRight = spriteRenderer.bounds.center.x + spriteRenderer.bounds.extents.x;
			if (spriteRight < cameraLeft) {
				child.Translate (new Vector3(spriteRenderer.bounds.size.x * 2, 0));
			}
		}
	}
}
