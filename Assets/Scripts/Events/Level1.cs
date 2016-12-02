using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : LevelEvent {
	public float enemyOffset = 10.0f;

	override protected IEnumerator spawnEnemies () {
		SingleTween tween = new SingleTween ("Level_1_enter", 3f);

		spawnEnemy (tween);
		spawnEnemy (tween.OffsetByVector(new Vector3(0, enemyOffset, 0)));

		yield return new WaitForSeconds (0);
	}	
}
