using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour {
	private Queue<TweenProperties> tweenQueue;
	private bool tweenInProgress = false;

	public void QueueTweens (params TweenProperties[] tweens) {
		foreach (TweenProperties tween in tweens) {
			tweenQueue.Enqueue (tween);
		}
	}

	void OnDisable(){
		iTween.Stop (gameObject);
		StopCoroutine("Animate");
		tweenInProgress = false;
		tweenQueue.Clear ();
	}

	void Update(){
		if (tweenQueue.Count > 0 && !tweenInProgress) {
			tweenInProgress = true;
			StartCoroutine (Animate ());
		}
	}
		

	void Awake(){
		tweenQueue = new Queue<TweenProperties> ();
	}

	IEnumerator Animate(){
		if (tweenQueue.Count > 0) {
			TweenProperties tween = tweenQueue.Dequeue ();
			tween.Apply (gameObject, tweenQueue);
			yield return new WaitForSeconds (tween.getDuration());
		}
		tweenInProgress = false;
	}
}
