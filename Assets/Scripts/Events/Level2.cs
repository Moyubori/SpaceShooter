using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : LevelEvent {
	public float enterDuration = 3.0f;
	public float flyDuration = 2.0f;
	public float flyDelay = 2.0f;

	private float enemyOffset = 5.0f;

	override protected void spawnEnemies () {
		TweenProperties enter = new SingleTween ("simpleEnter", enterDuration);
		TweenProperties moveUp = new LoopTween (LoopTween.Loop.reverse, new SingleTween ("2_up", flyDuration, flyDelay));
		TweenProperties moveDown = new LoopTween (LoopTween.Loop.reverse, new SingleTween ("2_down", flyDuration, flyDelay));

		spawnEnemy (EnemyObjectPool.type_enemy2, enter.Clone().OffsetByY(enemyOffset), moveUp);
		spawnEnemy (EnemyObjectPool.type_enemy2, enter);
		spawnEnemy (EnemyObjectPool.type_enemy2, enter.Clone().OffsetByY(-enemyOffset), moveDown);
	}
}
