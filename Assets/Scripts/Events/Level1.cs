using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : LevelEvent {
	
	override protected IEnumerator spawnEnemies () {
		Transform enemy1 = spawnEnemy ();
		enemy1.GetComponent<MovementController>().QueuePath("Level_1_enter1", 4f);

		Transform enemy2 = spawnEnemy ();
		enemy2.GetComponent<MovementController>().QueuePath("Level_1_enter2", 4f);

		yield return new WaitForSeconds (0);
	}	
}
