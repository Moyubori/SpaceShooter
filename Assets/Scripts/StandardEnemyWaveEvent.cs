using UnityEngine;
using System.Collections;

public class StandardEnemyWaveEvent : LevelEvent {

	public ObjectPool enemyPool;
	public int enemiesToSpawn = 5;

	override protected IEnumerator EventActions(){
		Transform currentEnemy;
		while (enemiesToSpawn >= 0) {
			enemiesToSpawn--;
			currentEnemy = enemyPool.GetInstance (new Vector3(20,0,0));
			currentEnemy.GetComponent<MovementController> ().QueuePath ("SpawnToStdPatrolPath", 4f);
			currentEnemy.GetComponent<MovementController> ().QueuePath ("StdPatrolPath", 4f, "reverse");
			while (currentEnemy.gameObject.activeSelf) {
				yield return new WaitForSeconds (1f);
			}
			yield return new WaitForSeconds (1f);
		}
		Debug.Log("Event is finishing");
		FinishEvent ();
	}
}

