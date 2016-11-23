using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour {

	private class TweenProperties {
		public string pathName;
		public Vector3[] path;
		public float time;
		public string loop;

		private bool isReversed = false;

		public void ReversePath(){
			if (isReversed) {
				isReversed = false;
				path = iTweenPath.GetPath (pathName);
			} else {
				isReversed = true;
				path = iTweenPath.GetPathReversed (pathName);
			}
		}
	}

	private Queue<TweenProperties> tweenQueue;
	private bool tweenInProgress = false;

	// adds a new path with given properties into queue
	public void QueuePath (string pathName, float time, string loop = "none"){
		TweenProperties newTween = new TweenProperties ();
		newTween.pathName = pathName;
		newTween.path = iTweenPath.GetPath(pathName);
		newTween.time = time;
		newTween.loop = loop;
		tweenQueue.Enqueue (newTween);
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
			TweenProperties tempProperties = tweenQueue.Dequeue ();
			iTween.MoveTo (gameObject, iTween.Hash("path", tempProperties.path, "time", tempProperties.time, "movetopath", false));

			// reinsert tween back into queue if it's looping
			if (tempProperties.loop == "reverse") {
				tempProperties.ReversePath ();
				tweenQueue.Enqueue (tempProperties);
			} else if (tempProperties.loop == "plain"){
				tweenQueue.Enqueue (tempProperties);
			}

			yield return new WaitForSeconds (tempProperties.time);
		}
		tweenInProgress = false;
	}
}
