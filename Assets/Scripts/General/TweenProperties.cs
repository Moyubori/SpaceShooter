using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class TweenProperties : ICloneable {
	public float delay;

	public abstract object Clone();
	//schedules all tweens to iTween.MoveTo, may enqueue in the event of loops
	public abstract void Apply (GameObject target, Queue<TweenProperties> tweenQueue);
	public abstract void Reverse ();
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


	public override object Clone() {
		SingleTween result = new SingleTween ();
		result.delay = delay;
		result.path = (Vector3[])path.Clone ();
		result.time = time;
		return result;
	}
	public SingleTween() {}
	public SingleTween(string pathName, float time = 1.0f, float delay = 0.0f) {
		this.path = iTweenPath.GetPath(pathName);
		this.time = time;
		this.delay = delay;
	}

	public override void Apply (GameObject target, Queue<TweenProperties> tweenQueue) {
		Hashtable args = iTween.Hash ("path", path, "time", time, "delay", delay, "movetopath", true, "easeType", "linear");
		iTween.MoveTo (target, args);
	}

	public override void Reverse(){
		path = (Vector3[]) path.Clone ();
		System.Array.Reverse (path);
	}

	public override float getDuration() {
		return delay + time;
	}
		
	public override TweenProperties OffsetByXY (float offsetX, float offsetY) {
		SingleTween result = (SingleTween) this.Clone ();
		for (int i = 0; i < result.path.Length; i++) {
			result.path [i].x += offsetX;
			result.path [i].y += offsetY;
		}
		return result;
	}
}








//Container for SingleTweens (actually, nesting LoopTweens is possible), can work as plain or reversed loop
public class LoopTween : TweenProperties {
	public enum Loop {none, normal, reverse};
	private TweenProperties[] tweens;
	private Loop loop;

	public override object Clone() {
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

	public override void Reverse() {
		foreach (TweenProperties tween in tweens) {
			tween.Reverse ();
		}

		tweens = (TweenProperties[]) tweens.Clone ();
		System.Array.Reverse (tweens);
	}

	public override float getDuration() {
		float result = 0;
		foreach (TweenProperties tween in tweens) {
			result += tween.getDuration ();
		}
		return result;
	}

	public override TweenProperties OffsetByXY (float offsetX, float offsetY) {
		LoopTween result = (LoopTween) this.Clone ();
		for (int i = 0; i < result.tweens.Length; i++) {
			result.tweens[i] = result.tweens[i].OffsetByXY (offsetX, offsetY);
		}
		return result;
	}

}







