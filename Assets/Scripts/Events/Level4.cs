using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level4 : LevelEvent {

	override protected IEnumerator spawnEnemies () {
		for (int i = 0; i < 4; i++) {
			Transform enemy = spawnEnemy ();
//			enemy.GetComponent<MovementController>().QueuePath("Level_4_enter", 3f);
//			enemy.GetComponent<MovementController>().QueuePath("Level_4_divide_"+(i+1), 1f);
//			enemy.GetComponent<MovementController>().QueuePath("Level_4_cruise_"+(i+1), 2f, "reverse");
		}
		yield return new WaitForSeconds (0);
	}
}
