using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level3 : LevelEvent {
	public int enemiesToSpawn = 5;

	[Header("Time values")]
	public float enterDuration = 2.0f;
	public float loopDuration = 4.0f;
    public float spawnInterval = 1.0f;

	override protected void spawnEnemies () {
		for (int i = 0; i < enemiesToSpawn; i++) {
			TweenProperties enter = new SingleTween ("3_enter", enterDuration, i*spawnInterval);
			TweenProperties loop = new LoopTween(new SingleTween("3_loop", loopDuration, 0));

			spawnEnemy (enter, loop);
		}
	}
}
