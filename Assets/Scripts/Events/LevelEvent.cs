using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LevelEvent : Event {
	[SerializeField]
	private ObjectPool enemyPool;
	private List<Transform> enemies = new List<Transform>();

	override protected IEnumerator EventActions() {
		yield return spawnEnemies ();
		yield return waitForLevelEnd ();
	}

	protected abstract IEnumerator spawnEnemies ();
	protected Transform spawnEnemy() {
		Transform result = enemyPool.GetInstance();
		enemies.Add (result);
		return result;
	}

	private IEnumerator waitForLevelEnd() {
		while (enemies.Count > 0) {
			for(int i=0; i<enemies.Count; i++) {
				Transform enemy = enemies[i];
				if (!enemy.gameObject.activeSelf) {
					enemies.Remove(enemy);
					i--;
				}
			}
			yield return new WaitForSeconds(1f);
		}
		FinishEvent();
	}
}