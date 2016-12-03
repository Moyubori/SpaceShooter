using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : LevelEvent {
	public float enterDuration = 3.0f;
	public float enemyOffset = 5.0f;

	override protected void spawnEnemies () {
		TweenProperties enter = new SingleTween ("simpleEnter", enterDuration);

		spawnEnemy (enter.Clone().OffsetByY(enemyOffset));
		spawnEnemy (enter.Clone().OffsetByY(-enemyOffset));
	}	
}
