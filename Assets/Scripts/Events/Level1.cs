using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : LevelEvent {
	public float enemyOffset = 5.0f;

	override protected void spawnEnemies () {
		SingleTween tween = new SingleTween ("simpleEnter", 3f);

		spawnEnemy (tween.OffsetByY(enemyOffset));
		spawnEnemy (tween.OffsetByY(-enemyOffset));
	}	
}
