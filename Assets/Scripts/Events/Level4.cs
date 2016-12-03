using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level4 : LevelEvent {
	private float cruiseOffset = 4.0f;

	[Header("Time values")]
	public float enterDuration = 1.0f;
	public float divideDuration = 1.0f;
	public float cruiseDuration = 1.0f;
	public float cruiseDelay = 0.5f;

	override protected void spawnEnemies () {
		TweenProperties enter = new SingleTween ("simpleEnter", enterDuration);
		TweenProperties cruiseUp = new LoopTween (LoopTween.Loop.reverse, new SingleTween ("4_cruiseUp", cruiseDuration, cruiseDelay));
		TweenProperties cruiseDown = new LoopTween (LoopTween.Loop.reverse, new SingleTween ("4_cruiseDown", cruiseDuration, cruiseDelay));

		for (int i = 1; i <= 4; i++) {
			TweenProperties divide = new SingleTween ("4_divide" + i, divideDuration);

			//first two enemies use cruiseUp tween
			TweenProperties cruise = (i <= 2) ? cruiseUp : cruiseDown;
			//second and third are translated down by cruiseOffset
			if (i % 2 == 0) {
				cruise = cruise.Clone().OffsetByY (-cruiseOffset);
			}

			spawnEnemy (enter, divide, cruise);
		}
	}
}
