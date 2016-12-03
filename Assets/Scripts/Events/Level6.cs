using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level6 : LevelEvent {
	public int enemiesToSpawn = 3;
	public float loopDuration = 3.0f;

	override protected void spawnEnemies () {
		float spawnInterval = loopDuration / enemiesToSpawn;
		for (int i = 0; i < enemiesToSpawn; i++) {
			TweenProperties startDelay = new SingleTween ("6_loop", loopDuration, i * spawnInterval);
			TweenProperties loop = new LoopTween(new SingleTween("6_loop", loopDuration));
			spawnEnemy (startDelay, loop);
		}
	}	
}

