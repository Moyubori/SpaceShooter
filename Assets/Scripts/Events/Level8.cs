using UnityEngine;
using System.Collections;

public class Level8 : LevelEvent {
	private float enemyDistance = 2.0f;

	public int enemiesToSpawn = 5;

	[Header("Time values:")]
	public float enterMaxDelay = 0.7f;
	public float enterDuration = 1.27f;
	public float cruiseDuration = 0.15f;
	public float cruiseDelay = 0.78f;

	protected override void spawnEnemies () {
//		TweenProperties enterPath = new SingleTween ("8_enter", enterDuration);
		TweenProperties cruisePath = new SingleTween ("8_cruise", cruiseDuration, cruiseDelay);
		TweenProperties cruisePathRev = cruisePath.Clone ().Reverse ();

		for (int i = 0; i < enemiesToSpawn; i++) {
			float yOffset = -i * enemyDistance;

			int enterPathIndex = ((i % 3) + 1);
			float enterDelay = enterMaxDelay * Random.value;
			TweenProperties preEnterDelay = new DelayTween (enterDelay);
			TweenProperties enter = new SingleTween ("8_enter" + enterPathIndex, enterDuration).OffsetByY (yOffset);
			TweenProperties postEnterDelay = new DelayTween (enterMaxDelay - enterDelay);

			TweenProperties preCruiseDelay = new DelayTween ((enemiesToSpawn - i) * cruiseDuration);
			TweenProperties postCruiseDlay = new DelayTween (cruiseDuration * i);

			TweenProperties cruise = cruisePath.Clone ().OffsetByY (yOffset);
			TweenProperties cruiseRev = cruisePathRev.Clone ().OffsetByY (yOffset);

			TweenProperties loop = new LoopTween (preCruiseDelay, cruise, postCruiseDlay, postCruiseDlay, cruiseRev, preCruiseDelay);
			spawnEnemy (EnemyObjectPool.type_enemy2, preEnterDelay, enter, postEnterDelay, loop);
		}
	}
}
