using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level7 : LevelEvent {
	private static int enemiesToSpawn = 5;

	[Header("Time values:")]
	public float enterDuration = 0.8f;
	public float enterMaxDelay = 0.4f;

	public float loopDuration = 0.6f;
	public float loopDelay = 4.0f;

	public float uniteDuration = 1.2f;

	override protected void spawnEnemies () {
		TweenProperties uniteUp = new SingleTween ("7_uniteUp", uniteDuration, 0, true);
		TweenProperties uniteDown = new SingleTween ("7_uniteDown", uniteDuration, 0, true);

		for (int i = 1; i <= enemiesToSpawn; i++) {
			
			float enterDelay = enterMaxDelay * Random.value;
			TweenProperties enter = new SingleTween ("7_enter"+i, enterDuration, enterDelay);
			TweenProperties postEnterDelay = new DelayTween (enterMaxDelay - enterDelay); //so all enemies start loop simultaneously

			TweenProperties loopDelay = new DelayTween (this.loopDelay);

			TweenProperties preUniteDelay = new DelayTween (uniteDuration * (i-1) / enemiesToSpawn);
			TweenProperties postUniteDelay = new DelayTween (uniteDuration * (enemiesToSpawn - i) / enemiesToSpawn);

			TweenProperties cruise = new SingleTween ("7_cruise"+i, loopDuration, 0, true);

			TweenProperties loop = new LoopTween (preUniteDelay, uniteUp, postUniteDelay, cruise, loopDelay, 
				preUniteDelay, uniteDown, postUniteDelay, cruise, loopDelay);

			spawnEnemy (EnemyObjectPool.type_enemy1, enter, postEnterDelay, loop);
		}
	}	
}