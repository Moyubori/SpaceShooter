using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class TweenProperties {
	public float delay;

	//schedules all tweens to iTween.MoveTo, may enqueue in the event of loops
	public abstract void Apply (GameObject target, Queue<TweenProperties> tweenQueue);
	public abstract void Reverse ();
	public abstract float getDuration();

	//returns a copied instance with all points translated by the offset
	public abstract TweenProperties OffsetByVector (Vector3 offset);
}



//actual tween which holding iTween data
public class SingleTween : TweenProperties {
	public Vector3[] path;
	public float time;

	public SingleTween(SingleTween instanceToCopy) {
		delay = instanceToCopy.delay;
		path = (Vector3[]) instanceToCopy.path.Clone ();
		time = instanceToCopy.time;
	}
	public SingleTween(string pathName, float time = 1.0f, float delay = 0.0f) {
		this.path = iTweenPath.GetPath(pathName);
		this.time = time;
		this.delay = delay;
	}

	public override void Apply (GameObject target, Queue<TweenProperties> tweenQueue) {
		Hashtable args = iTween.Hash ("path", path, "time", time, "delay", delay, "movetopath", false, "easeType", "linear");
		iTween.MoveTo (target, args);
	}

	public override void Reverse(){
		path = (Vector3[]) path.Clone ();
		System.Array.Reverse (path);
	}

	public override float getDuration() {
		return delay + time;
	}

	public override TweenProperties OffsetByVector (Vector3 offset) {
		SingleTween result = new SingleTween (this);
		for (int i = 0; i < result.path.Length; i++) {
			result.path [i] += offset;
		}
		return result;
	}
}



//Container for SingleTweens (actually, nesting LoopTweens is possible), can work as plain or reversed loop
public class LoopTween : TweenProperties {
	private TweenProperties[] tweens;
	private iTween.LoopType loopType;

	public LoopTween (LoopTween instanceToCopy) {
		delay = instanceToCopy.delay;
		tweens = (TweenProperties[]) instanceToCopy.tweens.Clone ();
		loopType = instanceToCopy.loopType;
	}
	public LoopTween (params TweenProperties[] tweens) : this(iTween.LoopType.loop, tweens) {}
	public LoopTween (iTween.LoopType loopType, params TweenProperties[] tweens) {
		this.loopType = loopType;
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
		switch (loopType) {
		case iTween.LoopType.loop:
			tweenQueue.Enqueue (this);
			break;

		case iTween.LoopType.pingPong:
			Reverse ();
			tweenQueue.Enqueue (this);
			break;

		case iTween.LoopType.none:
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

	public override TweenProperties OffsetByVector (Vector3 offset) {
		LoopTween result = new LoopTween (this);
		foreach (TweenProperties tween in tweens) {
			tween.OffsetByVector (offset);
		}
		return result;
	}
}







