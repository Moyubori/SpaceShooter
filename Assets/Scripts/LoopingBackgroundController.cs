using UnityEngine;
using System.Collections;

public class LoopingBackgroundController : BackgroundController {
	override protected void UpdateChildren (float cameraOffset) {
		foreach (Transform child in transform) {
			if (child.gameObject.activeSelf) {
				UpdateParallax (child, cameraOffset);
			}
		}

		//Transform sprite_1 = child.GetChild (0);
		//Transform sprite_2 = child.GetChild (1);

		//LoopSprite (sprite_1, sprite_2);
		//LoopSprite (sprite_2, sprite_1);
	}

	//Checks if 'looped' sprite left visible area //TODO it doesn't work...
	private void LoopSprite(Transform sprite, Transform second) {
		float cameraLeft = camera.transform.position.x - camera.orthographicSize * camera.aspect;
		float spriteLeft = sprite.position.x - sprite.GetComponent<SpriteRenderer> ().bounds.size.x / 2;

		if (spriteLeft > cameraLeft) {
			float secondWidth = second.GetComponent<SpriteRenderer> ().bounds.size.x;

			Vector3 position = second.position;
			position.x = cameraLeft - secondWidth / 2;
			second.position = position;
		}
	}
}
