using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : LevelEvent {
    private static int ENEMY_COUNT = 3;

	override protected IEnumerator spawnEnemies () {
		for (int i = 0; i < ENEMY_COUNT; i++) {
			Transform enemy = spawnEnemy ();
			enemy.GetComponent<MovementController>().QueuePath("Level_2_enter"+(i + 1), 1f, "none");
			enemy.GetComponent<MovementController>().QueuePath("Level_2_move"+ (i + 1), 4f, "reverse", 2);
		}
		yield return new WaitForSeconds (0);
	}
}
