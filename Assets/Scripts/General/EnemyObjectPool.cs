using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyObjectPool : ObjectPool {
	public static readonly int TYPE_COUNT = 3;

	public static readonly EnemyType type_enemy1 = new EnemyType(0);
	public static readonly EnemyType type_enemy2 = new EnemyType(1);
	public static readonly EnemyType type_boss = new EnemyType(2);

	[Header("Order: enemy1, enemy2, boss")]
	public Transform[] enemyPrefabs = new Transform[TYPE_COUNT];

	private List<Transform>[] pools;

	void Start () {
		pools = new List<Transform>[TYPE_COUNT];
		for (int i = 0; i < pools.Length; i++) {
			pools [i] = new List<Transform> ();
		}
	}
		
	public Transform GetInstance(EnemyType type) {
		List<Transform> instances = pools [type.index];

		//search for already created inactive (available) instances
		foreach (Transform child in instances) {
			if (!child.gameObject.activeSelf) {
				child.gameObject.SetActive (true);
				return child;
			}
		}

		//if not found, create new instance
		Transform prefab = enemyPrefabs [type.index];
		Transform newInstance = (Transform)Instantiate (prefab, transform);
		instances.Add (newInstance);
		return newInstance;
	}

	public Transform GetInstance(EnemyType type, Vector3 position) {
		Transform result = GetInstance (type);
		result.position = position;
		return result;

	}
}

public class EnemyType {
	public EnemyType(int index) {
		this.index = index;
	}

	public readonly int index;
}
