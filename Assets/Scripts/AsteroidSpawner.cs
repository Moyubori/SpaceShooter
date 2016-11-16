using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public ObjectPool asteroidPool;
	public int maxAsteroids = 5;

	private void TryToDeployAnAsteroid(){
		if (asteroidPool.InstancesActive () < maxAsteroids) {
			Vector3 spawnPosition = new Vector3 (transform.localPosition.x, Random.Range (-7, 7), transform.localPosition.z);
			asteroidPool.GetInstanceRelative (spawnPosition);
		}
	}

	void Start(){
		InvokeRepeating ("TryToDeployAnAsteroid", 0, 3);
	}
}
