using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LevelEvent : Event {
	private List<Transform> enemies = new List<Transform>();
	private LevelManager levelManager;

	void Awake() {
		levelManager = transform.parent.GetComponent<LevelManager> ();
	}

	override protected IEnumerator EventActions() {
		spawnEnemies ();
		yield return waitForLevelEnd ();
	}

	//main method spawning all the enemies
	protected abstract void spawnEnemies ();

	//method invoked once per enemy, spawns at given position and aplies tween
	//spawns at default position, which is beyond the top-right corner of the screen
	protected Transform spawnEnemy(EnemyType type, params TweenProperties[] tweens) {
		Transform result = levelManager.enemyPool.GetInstance (type);

		Camera cam = levelManager.camera;
		Vector3 enemyExtents = result.GetComponentInChildren<SpriteRenderer> ().bounds.extents;
		result.position = calculatePosition (cam, enemyExtents);
		result.GetComponent<MovementController> ().QueueTweens (tweens);
		enemies.Add (result);
		return result;
	}
	private Vector3 calculatePosition(Camera cam, Vector3 enemyExtents) {
		float cameraTop = cam.transform.position.y + cam.orthographicSize;
		float cameraRight = cam.transform.position.x + cam.orthographicSize * cam.aspect;
		return new Vector3 (cameraRight + enemyExtents.x, cameraTop + enemyExtents.y);
	}
		
	//method invoked once per enemy, spawns at given position and aplies tween
	//spawn at specified position
	protected Transform spawnEnemy(Vector3 position, EnemyType type, params TweenProperties[] tweens) {
		Transform result = levelManager.enemyPool.GetInstance(type, position);
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
			yield return new WaitForSeconds(0.3f);
		}
		FinishEvent();
	}

}
