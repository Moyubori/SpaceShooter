using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level4 : LevelEvent {
	private static float enterDuration = 3.0f;
	private static float divideDuration = 1.0f;

	public float cruiseOffset = 4.0f;

	override protected void spawnEnemies () {
		SingleTween enter = new SingleTween ("simpleEnter", enterDuration);
		LoopTween cruiseUp = new LoopTween (LoopTween.Loop.reverse, new SingleTween ("4_cruiseUp", divideDuration));
		LoopTween cruiseDown = new LoopTween (LoopTween.Loop.reverse, new SingleTween ("4_cruiseDown", divideDuration));

		for (int i = 1; i <= 4; i++) {
			SingleTween divide = new SingleTween ("4_divide" + i, 1f);

			//first two enemies use cruiseUp tween
			LoopTween cruise = (i <= 2) ? cruiseUp : cruiseDown;
			//second and third are translated down by cruiseOffset
			if (i % 2 == 0) {
				cruise = (LoopTween)cruise.OffsetByY (-cruiseOffset);
			}

			spawnEnemy (enter, divide, cruise);
		}
	}
}
