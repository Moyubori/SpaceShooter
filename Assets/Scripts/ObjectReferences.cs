using UnityEngine;
using System.Collections;

public class ObjectReferences : MonoBehaviour {

	public static Transform objectPools;
	public static Transform player;
	public static Transform background;
	public static Transform hudElements;

	[SerializeField]
	private Transform _objectPools;
	[SerializeField]
	private Transform _player;
	[SerializeField]
	private Transform _background;
	[SerializeField]
	private Transform _hudElements;

	void Awake(){
		objectPools = _objectPools;
		player = _player;
		background = _background;
		hudElements = _hudElements;
	}

}
