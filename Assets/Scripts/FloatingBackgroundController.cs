using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingBackgroundController : BackgroundController {

	//TimedUpdate parameters
	private static float TIMED_UPDATE_DELAY = 0;
	private static float TIMED_UPDATE_INTERVAL = 0.5f;

	//distance between planets
	private static float PLANETS_DISTANCE = 6.0f;

	public ObjectPool backgroundPool;
	public string resourcePath;
	private Sprite[] sprites;

	void Start() {
		LoadResources (resourcePath);
		InvokeRepeating ("TimedUpdate", TIMED_UPDATE_DELAY, TIMED_UPDATE_INTERVAL);
		InitialSpawn ();
	}

	//makes it easier to load different file later
	void LoadResources(string path) {
		sprites = Resources.LoadAll<Sprite> (path);
	}
		
	//called each frame in base class' Update();
	override protected void UpdateChildren (float cameraOffset) {
		foreach (Transform child in backgroundPool.transform) {
			if (child.gameObject.activeSelf) {
				UpdateParallax (child, cameraOffset);
			}
		}
	}

	//called in timed intervals
	private void TimedUpdate() {
		CheckObjectsDespawn ();
		CheckObjectsSpawn ();
	}
		
	void InitialSpawn () {
		float cameraWidth = camera.orthographicSize * camera.aspect;
		for (float x = 0; x < cameraWidth * 2; x += PLANETS_DISTANCE) {
			Sprite sprite = GetRandomSprite ();

			Vector3 pos = new Vector3 ();
			pos.x = x - cameraWidth;
			pos.y = CalculatePositionY ();
			pos.z = CalculatePositionZ (sprite);

			SpawnObject (pos, sprite);
		}
	}





	private void CheckObjectsDespawn() {
		foreach (Transform child in backgroundPool.transform) {
			Bounds bounds = child.GetComponent<SpriteRenderer> ().bounds;
			float cameraX = camera.transform.position.x;
			float cameraWidth = camera.orthographicSize * camera.aspect;
			if (bounds.max.x < cameraX - cameraWidth /*|| bounds.min.x > cameraX + cameraWidth*/) {
				child.gameObject.SetActive (false);
			}
		}
	}






	private void CheckObjectsSpawn () {
		float maxPosition = float.MinValue;
		foreach (Transform child in backgroundPool.transform) {
			float x = child.position.x;
			if (x > maxPosition) {
				maxPosition = x;
			}
		}

		float cameraRight = camera.transform.position.x + camera.orthographicSize * camera.aspect;
		if (maxPosition + PLANETS_DISTANCE < cameraRight) {
			SpawnObject ();
		}
	}

	//Spawns on the right side of the screen
	private void SpawnObject() {
		Sprite sprite = GetRandomSprite ();

		Vector3 position = new Vector3 ();
		position.x = CalculatePositionX (sprite);
		position.y = CalculatePositionY ();
		position.z = CalculatePositionZ (sprite);

		SpawnObject (position, sprite);
	}

	private void SpawnObject(Vector3 position, Sprite sprite) {
		Transform instance = backgroundPool.GetInstance ();
		instance.gameObject.name = "BGSprite_" + sprite.name;

		SpriteRenderer renderer = instance.GetComponent<SpriteRenderer> ();
		renderer.sprite = sprite;
		renderer.sortingLayerName = "Background";
		renderer.sortingOrder = 2;

		instance.position = position;
		instance.rotation = Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360)));
	}

	private float CalculatePositionX(Sprite sprite) {
		return transform.position.x + sprite.bounds.extents.x + camera.orthographicSize * camera.aspect;
	}

	private float CalculatePositionY() {
		return Random.Range(-1f, 1f) * camera.orthographicSize;
	}

	//Small object => it is far away => it moves slowly => it has big position.z
	private static float CalculatePositionZ(Sprite sprite) {
		//these 4 values are adjustable parameters
		const float minSize = 0, maxSize = 8;
		const float minZ = 100, maxZ = 350;

		float size = sprite.rect.size.x / sprite.pixelsPerUnit;
		float t = Mathf.InverseLerp (minSize, maxSize, size);
		float z = Mathf.Lerp (minZ, maxZ, 1-t);
		return z;

	}

	//TODO prevent having many similar sprites at once
	private Sprite GetRandomSprite() {
		int count = sprites.Length;
		return (count > 0) ? sprites[Random.Range (0, count)] : null;
	}
}