using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class TweenProperties {
	public float delay;

	public abstract TweenProperties Clone ();
	//schedules all tweens to iTween.MoveTo, may enqueue in the event of loops
	public abstract void Apply (GameObject target, Queue<TweenProperties> tweenQueue);
	public abstract TweenProperties Reverse ();
	public abstract float getDuration();

	//offsetting returns new instance with all points translated
	public TweenProperties OffsetByX (float offsetX) {
		return OffsetByXY (offsetX, 0);
	}
	public TweenProperties OffsetByY (float offsetY) {
		return OffsetByXY (0, offsetY);
	}
	public TweenProperties OffsetByXY (Vector2 offset) {
		return OffsetByXY (offset.x, offset.y);
	}
	public abstract TweenProperties OffsetByXY (float offsetX, float offsetY);

}








//actual tween which holding iTween data
public class SingleTween : TweenProperties {
	public Vector3[] path;
	public float time;
	public bool moveToPath;

	public override TweenProperties Clone() {
		SingleTween result = new SingleTween ();
		result.delay = delay;
		result.path = (Vector3[])path.Clone ();
		result.time = time;
		result.moveToPath = moveToPath;
		return result;
	}
	public SingleTween() {}
	public SingleTween(string pathName, float time = 1.0f, float delay = 0.0f, bool moveToPath = false) {
		this.path = iTweenPath.GetPath(pathName);
		this.time = time;
		this.delay = delay;
		this.moveToPath = moveToPath;
	}

	public override void Apply (GameObject target, Queue<TweenProperties> tweenQueue) {
		Hashtable args = iTween.Hash ("path", path, "time", time, "delay", delay, "movetopath", moveToPath, "easeType", "linear");
		iTween.MoveTo (target, args);
	}

	public override TweenProperties Reverse(){
		path = (Vector3[]) path.Clone ();
		System.Array.Reverse (path);
		return this;
	}

	public override float getDuration() {
		return delay + time;
	}
		
	public override TweenProperties OffsetByXY (float offsetX, float offsetY) {
		for (int i = 0; i < path.Length; i++) {
			path [i].x += offsetX;
			path [i].y += offsetY;
		}
		return this;
	}

	public System.Collections.Hashtable GetHash(){
		Hashtable args = iTween.Hash ("path", path, "time", time, "delay", delay, "movetopath", moveToPath, "easeType", "linear");
		return args;
	}
}








//Container for SingleTweens (actually, nesting LoopTweens is possible), can work as plain or reversed loop
public class LoopTween : TweenProperties {
	public enum Loop {none, normal, reverse};
	private TweenProperties[] tweens;
	private Loop loop;

	public override TweenProperties Clone() {
		TweenProperties[] tweens = new TweenProperties[this.tweens.Length];
		for(int i=0; i<tweens.Length; i++) {
			tweens [i] = (TweenProperties) this.tweens [i].Clone ();
		}
		return new LoopTween (delay, loop, tweens);
	}
	public LoopTween () {}
	public LoopTween (params TweenProperties[] tweens) : this(0, Loop.normal, tweens) {}
	public LoopTween (float delay, params TweenProperties[] tweens) : this(delay, Loop.normal, tweens) {}
	public LoopTween (Loop loop, params TweenProperties[] tweens) : this (0, loop, tweens) {}
	public LoopTween (float delay, Loop loop, params TweenProperties[] tweens) {
		this.delay = delay;
		this.loop = loop;
		this.tweens = tweens;
	}

	public override void Apply (GameObject target, Queue<TweenProperties> tweenQueue) {
		float delay = this.delay;
		foreach (TweenProperties tween in tweens) {
			tween.delay += delay;
			tween.Apply (target, tweenQueue);
			tween.delay -= delay;

			delay += tween.getDuration ();
		}
		ApplyLoop (tweenQueue);
	}

	private void ApplyLoop(Queue<TweenProperties> tweenQueue) {
		switch (loop) {
		case Loop.normal:
			tweenQueue.Enqueue (this);
			break;

		case Loop.reverse:
			Reverse ();
			tweenQueue.Enqueue (this);
			break;

		case Loop.none:
			break;
		}
	}

	public override TweenProperties Reverse() {
		foreach (TweenProperties tween in tweens) {
			tween.Reverse ();
		}

		tweens = (TweenProperties[]) tweens.Clone ();
		System.Array.Reverse (tweens);
		return this;
	}

	public override float getDuration() {
		float result = 0;
		foreach (TweenProperties tween in tweens) {
			result += tween.getDuration ();
		}
		return result;
	}

	public override TweenProperties OffsetByXY (float offsetX, float offsetY) {
		for (int i = 0; i < tweens.Length; i++) {
			tweens[i] = tweens[i].OffsetByXY (offsetX, offsetY);
		}
		return this;
	}

}






public class DelayTween : TweenProperties {

	public override TweenProperties Clone() {
		return new DelayTween (delay);
	}
	public DelayTween (float delay) {
		this.delay = delay;
	}
		
	public override TweenProperties Reverse () {
		return this;
	}

	public override float getDuration() {
		return delay;
	}
		
	public override TweenProperties OffsetByXY (float offsetX, float offsetY) {
		return this;
	}

	public override void Apply (GameObject target, Queue<TweenProperties> tweenQueue) {}
}