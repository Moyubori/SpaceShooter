using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level5 : LevelEvent {
	private static int enemiesToSpawn = 4;
	private static float horizontalOffset = 2.0f;
	private static float verticalOffset = 2.0f;

	[Header("Time values")]
	public float enterDuration = 0.8f;
	public float cruiseDelay = 2.0f;
	public float cruiseDuration = 0.7f;

	override protected void spawnEnemies () {
		TweenProperties enterUp = new SingleTween ("5_enterUp", enterDuration);
		TweenProperties enterDown = new SingleTween ("5_enterDown", enterDuration);
		TweenProperties cruiseUp = new LoopTween (LoopTween.Loop.reverse, new SingleTween("5_cruiseUp", cruiseDuration, cruiseDelay));
		TweenProperties cruiseDown = new LoopTween (LoopTween.Loop.reverse, new SingleTween("5_cruiseDown", cruiseDuration, cruiseDelay));


		for (int i = 1; i <= enemiesToSpawn; i++) {
			bool left = (i == 2 || i == 3);
			bool up = (i <= 2);
			TweenProperties enter  = (up ? enterUp : enterDown).Clone ();
			TweenProperties cruise = (up ? cruiseUp : cruiseDown).Clone ();

			if (left) {
				Vector2 offset = new Vector2 (-horizontalOffset, (up ? -1 : 1) * verticalOffset);

				enter.OffsetByXY (offset);
				cruise.OffsetByXY (offset);
			}



			EnemyType type = (left) ? EnemyObjectPool.type_enemy1 : EnemyObjectPool.type_enemy2;
			spawnEnemy (type, enter, cruise);	
		}
	}	
}

