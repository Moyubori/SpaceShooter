using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : LevelEvent {
	private static float enterDuration = 3.0f;
	private static float flyDuration = 2.0f;
	private static float flyDelay = 2.0f;

	public float enemyOffset = 5.0f;

	override protected void spawnEnemies () {
		SingleTween enter = new SingleTween ("simpleEnter", enterDuration);
		LoopTween moveUp = new LoopTween (1.0f, LoopTween.Loop.reverse, new SingleTween ("2_up", flyDuration, flyDelay));
		LoopTween moveDown = new LoopTween (1.0f, LoopTween.Loop.reverse, new SingleTween ("2_down", flyDuration, flyDelay));

		spawnEnemy (enter.OffsetByY(enemyOffset), moveUp);
		spawnEnemy (enter);
		spawnEnemy (enter.OffsetByY(-enemyOffset), moveDown);
	}
}
