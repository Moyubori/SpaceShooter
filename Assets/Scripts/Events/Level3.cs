using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level3 : LevelEvent {

    public int enemiesToSpawn = 5;
    public float spawnInterval = 1.0f;

	override protected IEnumerator spawnEnemies () {
		for (int i = 0; i < enemiesToSpawn; i++) {
			Transform enemy = spawnEnemy ();
//			enemy.GetComponent<MovementController>().QueuePath("Level_3_enter", 4f, "none");
//			enemy.GetComponent<MovementController>().QueuePath("Level_3_loop",  4f, "plain");
			yield return new WaitForSeconds(spawnInterval);
		}
		yield return new WaitForSeconds (0);
	}
}
