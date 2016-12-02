using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : LevelEvent {
    private static int ENEMY_COUNT = 3;

	public float enemyOffset = 5.0f;

	override protected IEnumerator spawnEnemies () {
		SingleTween enter = new SingleTween ("Level_2_enter", 1.5f);
		LoopTween moveUp = new LoopTween (iTween.LoopType.pingPong, new SingleTween ("Level_2_moveUp", 1.8f));
		moveUp.delay = 1.0f;
		LoopTween moveDown = new LoopTween (iTween.LoopType.pingPong, new SingleTween ("Level_2_moveDown", 2.1f));
		moveDown.delay = 1.0f;

		spawnEnemy (enter.OffsetByVector (new Vector3 (0, enemyOffset)), moveUp);
		spawnEnemy (enter);
		spawnEnemy (enter.OffsetByVector (new Vector3 (0, -enemyOffset)), moveDown);

		for (int i = 0; i < ENEMY_COUNT; i++) {
			Transform enemy = spawnEnemy ();
//			enemy.GetComponent<MovementController>().QueuePath("Level_2_enter"+(i + 1), 1f, "none");
//			if(i != 1) enemy.GetComponent<MovementController>().QueuePath("Level_2_move"+ (i + 1), 2.5f, "reverse");
		}
		yield return new WaitForSeconds (0);
	}
}
