using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level3 : LevelEvent {

    public int enemiesToSpawn = 5;
    public float spawnInterval = 1.0f;

	override protected void spawnEnemies () {
		for (int i = 0; i < enemiesToSpawn; i++) {
			SingleTween enter = new SingleTween ("3_enter", 4f, i*spawnInterval);
			LoopTween loop = new LoopTween(new SingleTween("3_loop", 4f));

			spawnEnemy (enter, loop);
		}
	}
}
