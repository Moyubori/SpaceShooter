using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour {

	public Transform objectPrefab;

	//seeks for existing inactive projectile instance or creates new one
	public Transform GetInstance() {
		foreach (Transform child in transform) {
			if (!child.gameObject.activeSelf) {
				child.gameObject.SetActive (true);
				return child;
			}
		}
		Transform newObject = (Transform)Instantiate (objectPrefab.transform, transform);
		return newObject;
	}

	// returns the instance of the prefab and sets it to given position and rotation
	public Transform GetInstance(Vector3 position, Quaternion rotation){
		Transform result = GetInstance ();
		result.position = position;
		result.rotation = rotation;
		return result;
	}

	public Transform GetInstance(Vector3 position) {
		Transform result = GetInstance ();
		result.position = position;
		return result;
	}

	// returns the instance of the prefab and sets it to given local position (relative to the center of the camera)
	public Transform GetInstanceRelative(Vector3 position, Quaternion rotation){
		Transform result = GetInstance ();
		result.localPosition = position;
		result.rotation = rotation;
		return result;
	}

	public Transform GetInstanceRelative(Vector3 position) {
		Transform result = GetInstance ();
		result.localPosition = position;
		return result;
	}

	// returns number of currently active objects in the pool
	public int InstancesActive(){
		int counter = 0;
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).gameObject.activeSelf) {
				counter++;
			}
		}
		return counter;
	}
}
