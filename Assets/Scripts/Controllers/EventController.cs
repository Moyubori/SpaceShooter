using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventController : MonoBehaviour {

	private Queue<LevelEvent> eventQueue;
	public Transform levels;

	private LevelEvent currentEvent = null;

	void Awake(){
		eventQueue = new Queue<LevelEvent> ();
	}

	void Start() {
		LevelEvent[] levels = this.levels.GetComponentsInChildren<LevelEvent> ();
		foreach(LevelEvent level in levels){
			eventQueue.Enqueue (level);
		}

		InvokeRepeating ("TryToLaunchNextEvent", 0, 1f);
	}


	public void QueueEvent(LevelEvent newEvent){
		eventQueue.Enqueue (newEvent);
	}

	private void TryToLaunchNextEvent(){
		// Launch new event if one has not been set yet or the current one finished
		if (currentEvent == null || currentEvent.finished) {
			StartNextEvent ();
		}
	}

	private void StartNextEvent() {
		if (eventQueue.Count > 0) {
			currentEvent = eventQueue.Dequeue ();
			currentEvent.LaunchEvent ();
			Debug.Log ("Starting event: " + currentEvent.name);
		} else {
			currentEvent = null;
			Debug.Log ("No events to launch");
		}
	}



}
