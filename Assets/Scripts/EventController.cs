using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventController : MonoBehaviour {

	private Queue<LevelEvent> eventQueue;
	public LevelEvent[] eventArray;

	private LevelEvent currentEvent;

	public void QueueEvent(LevelEvent newEvent){
		eventQueue.Enqueue (newEvent);
	}

	private void TryToLaunchNextEvent(){
		if (currentEvent == null) {
			if (eventQueue.Count > 0) {
				currentEvent = eventQueue.Dequeue ();
			} else {
				Debug.Log ("No events to launch");
				return;
			}
		} else {
			if (currentEvent.running) {
				return;
			} else if (currentEvent.finished) {
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

		InvokeRepeating ("TryToLaunchNextEvent", 0, 3f);
	}

}
