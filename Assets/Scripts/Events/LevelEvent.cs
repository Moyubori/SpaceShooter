using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LevelEvent : Event {
	private List<Transform> enemies = new List<Transform>();

	override protected IEnumerator EventActions() {
		spawnEnemies ();
		yield return waitForLevelEnd ();
	}

	//main method spawning all the enemies
	protected abstract void spawnEnemies ();

	//spawns at default position, which is beyond the right side of the screen
	protected Transform spawnEnemy(params TweenProperties[] tweens) {
		Camera cam = getLevelManager ().camera;
		Vector3 enemyExtents = getLevelManager ().enemyPool.objectPrefab.GetComponentInChildren<SpriteRenderer> ().bounds.extents;

		float cameraTop = cam.transform.position.y + cam.orthographicSize;
		float cameraRight = cam.transform.position.x + cam.orthographicSize * cam.aspect;
		Vector3 position = new Vector3 (cameraRight + enemyExtents.x, cameraTop + enemyExtents.y);

		return spawnEnemy (position, tweens);
	}

	//method invoked once per enemy, spawns at given position and aplies tween
	protected Transform spawnEnemy(Vector3 position, params TweenProperties[] tweens) {
		Transform result = getLevelManager().enemyPool.GetInstance(position);
		result.GetComponent<MovementController> ().QueueTweens (tweens);
		enemies.Add (result);
		return result;
	}

	//wait for all enemies to become inactive
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

	private LevelManager getLevelManager() {
		return transform.parent.GetComponent<LevelManager> ();
	}
}