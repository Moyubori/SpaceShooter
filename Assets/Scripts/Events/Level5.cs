using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level5 : LevelEvent {
	private static float horizontalOffset = 2.3f;
	private static float verticalOffset = 1.0f;
	private static float enterDuration = 2.0f;

	override protected void spawnEnemies () {
		SingleTween enterUp = new SingleTween ("5_enterUp", enterDuration);
		SingleTween enterDown = new SingleTween ("5_enterDown", enterDuration);
		SingleTween path = new SingleTween ("path", 2.0f, 1.0f);


		spawnEnemy (enterUp, path);
		spawnEnemy (enterDown, path);
		spawnEnemy (enterUp.OffsetByXY (-horizontalOffset, -verticalOffset), path);
		spawnEnemy (enterDown.OffsetByXY (-horizontalOffset, verticalOffset), path);

	}	
}

