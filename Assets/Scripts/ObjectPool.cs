using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour {

	public Transform objectPrefab;

	//seeks for existing inactive projectile instance or creates new one
	public Transform GetInstance(Vector3 position, Quaternion rotation){
		foreach (Transform child in transform) {
			if (!child.gameObject.activeSelf) {
				child.gameObject.SetActive (true);
				child.position = position;
				child.rotation = rotation;
				return child;
			}
		}
		Transform newObject = (Transform)Instantiate (objectPrefab.transform, transform);
		newObject.position = position;
		newObject.rotation = rotation;
		return newObject;
	}
}
