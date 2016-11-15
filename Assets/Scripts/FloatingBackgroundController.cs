using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO use object pool
public class FloatingBackgroundController : BackgroundController {
	private static float DELAY = 2.0f;
	private static float DEFAULT_RATE = 1.0f;
	private static float CHECK_RATE = 2.5f;
	public float spawnRate;

	void Start() {
		if (spawnRate == 0) {
			spawnRate = DEFAULT_RATE;
		}
		InvokeRepeating ("SpawnObject", DELAY, spawnRate);
		InvokeRepeating ("CheckObjectsAgainstCamera", DELAY, CHECK_RATE);
	}

	override protected void UpdateChildren (float cameraOffset) {
		foreach (Transform child in transform) {
			if (child.gameObject.activeSelf) {
				UpdateParallax (child, cameraOffset);
			}
		}
	}
		
	//Object spawning

	private void SpawnObject () {
		List<Transform> activeObjects = GetInactiveObjects ();

		if (activeObjects.Count > 0) {
			Transform toActivate = SelectObjectToActivate (activeObjects);
			Activate (toActivate);
		}
	}

	private List<Transform> GetInactiveObjects() {
		List<Transform> result = new List<Transform> ();
		foreach (Transform child in transform) {
			if (!child.gameObject.activeSelf) {
				result.Add (child);
			}
		}
		return result;
	}

	private Transform SelectObjectToActivate(List<Transform> list) {
		int index = Random.Range (0, list.Count - 1);
		return list [index];
	}

	private void Activate(Transform toActivate) {
		toActivate.gameObject.SetActive (true);

		Vector3 position = toActivate.parent.position;
		position.x += camera.orthographicSize * camera.aspect;
		position.y = Random.Range(-1f, 1f) * camera.orthographicSize;
		position.z = Random.Range(-1f, 1f) * 100 + 30; //TODO placeholder
		toActivate.position = position;
	}




	//Object de-spawning

	private void CheckObjectsAgainstCamera() {
		foreach (Transform child in transform) {
			float cameraX = camera.transform.position.x;
			float cameraY = camera.transform.position.y;
			float cameraHeight = camera.orthographicSize;
			float cameraWidth = cameraHeight * camera.aspect;
					
			//TODO take child's bounds into consideration
			if (child.position.x > cameraX + cameraWidth || 
				child.position.x < cameraX - cameraWidth ||
				child.position.y > cameraY + cameraHeight || 
				child.position.y < cameraY - cameraHeight) {
				//Debug.Log (child.name + ": pos[" + child.position.x + " ; " + child.position.y + "] cameraPos: [" + cameraX + " ; " + cameraY + "] cameraHalfSize: " + cameraWidth + " ; " + cameraHeight + "]");
				child.gameObject.SetActive (false);
			}
		}
	}
}
