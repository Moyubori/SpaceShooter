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
}
