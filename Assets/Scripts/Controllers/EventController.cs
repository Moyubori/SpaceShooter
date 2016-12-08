using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventController : MonoBehaviour {

	private Queue<Event> eventQueue;
	public Transform levels;

	public int skipEvents = 0;

	private Event currentEvent = null;

	void Awake(){
		eventQueue = new Queue<Event> ();
	}

	void Start() {
		Event[] levels = this.levels.GetComponentsInChildren<Event> ();
		foreach(Event level in levels){
			eventQueue.Enqueue (level);
		}
		GameData.gameData.Reset ();

		for(int i = 0; i < skipEvents; i++) {
			eventQueue.Dequeue ();
		}

		InvokeRepeating ("TryToLaunchNextEvent", 0, 1f);
	}


	public void QueueEvent(Event newEvent){
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
