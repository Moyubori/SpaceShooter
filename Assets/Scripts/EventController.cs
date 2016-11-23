using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventController : MonoBehaviour {

	private Queue<LevelEvent> eventQueue;
	public LevelEvent[] eventArray; // events from this array will be pushed to the queue

	private LevelEvent currentEvent;

	public void QueueEvent(LevelEvent newEvent){
		eventQueue.Enqueue (newEvent);
	}

	private void TryToLaunchNextEvent(){
		// check if should launch a new event
		if (currentEvent == null) { // no event was set
			if (eventQueue.Count > 0) {
				currentEvent = eventQueue.Dequeue ();
			} else {
				Debug.Log ("No events to launch");
				return;
			}
		} else {
			if (currentEvent.running) { // there is an active event
				return;
			} else if (currentEvent.finished) { // there was an active event, but it finished
				if (eventQueue.Count > 0) {
					currentEvent = eventQueue.Dequeue ();
				} else {
					Debug.Log ("No events to launch");
					return;
				}
			}
		}
		currentEvent.LaunchEvent ();
	}

	void Awake(){
		eventQueue = new Queue<LevelEvent> ();
	}

	void Start(){
		foreach(LevelEvent i in eventArray){
			eventQueue.Enqueue (i);
		}

		InvokeRepeating ("TryToLaunchNextEvent", 0, 1f);
	}
}
