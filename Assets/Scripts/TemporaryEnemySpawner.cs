using UnityEngine;
using System.Collections;

public class TemporaryEnemySpawner : MonoBehaviour {

	public ObjectPool pool;

	private Transform enemy;
	private bool tryToSpawnNewEnemy = true;

	private void RespawnEnemy(){
		Vector3 temp = transform.position;
		enemy = pool.GetInstance (temp, Quaternion.Euler(new Vector3(0,0,180)));
		enemy.GetComponent<MovementController> ().QueuePath ("SpawnToStdPatrolPath", 4f);
		enemy.GetComponent<MovementController> ().QueuePath ("StdPatrolPath", 4f, "reverse");
	}

	private void TryToSpawnNewEnemy(){
		if (enemy == null) {
			RespawnEnemy ();
		} else if (!enemy.gameObject.activeSelf) {
			RespawnEnemy ();
		}
	}

	void Start (){
		InvokeRepeating ("TryToSpawnNewEnemy", 0, 1);
	}
}
