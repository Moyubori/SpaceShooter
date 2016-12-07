using UnityEngine;
using System.Collections;

public class Level9 : LevelEvent {
	public float enterDuration = 3.0f;
	public float loopDelay = 1.0f;
	public float changeSideDuration = 5.0f;
	public float attackDuration = 1.5f;

	override protected void spawnEnemies () {
		TweenProperties enter = new SingleTween ("9_enter", enterDuration);
		TweenProperties postEnterDelay = new DelayTween (loopDelay);

		TweenProperties goDown = new SingleTween ("9_changeSide", changeSideDuration);
		TweenProperties goUp = goDown.Clone ().Reverse ();
		TweenProperties attackUp = new SingleTween ("9_attackUp", attackDuration);
		TweenProperties attackUpRev = attackUp.Clone ().Reverse ();
		TweenProperties attackDown = new SingleTween ("9_attackDown", attackDuration);
		TweenProperties attackDownRev = attackDown.Clone ().Reverse ();

		TweenProperties loop = new LoopTween (attackUp, attackUpRev, goDown, attackDown, attackDownRev, goUp);
		spawnEnemy (EnemyObjectPool.type_boss, enter, postEnterDelay, loop);
	}	
}
