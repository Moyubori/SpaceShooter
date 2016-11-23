using UnityEngine;
using System.Collections;

public class AbstractClasses : MonoBehaviour {}

public abstract class Enemy : MonoBehaviour {

	public int health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	protected int _health = 100;
	protected int defaultHealth;

	public abstract void TakeDamage (int damage);
}



public abstract class Weapon : MonoBehaviour {

	public ObjectPool projectilePool;
	public Transform projectileOrigin;

	[SerializeField]
	protected int weaponLevel = 0;

	public abstract void Shoot ();
}



public abstract class BackgroundController : MonoBehaviour {
	new public Camera camera;

	private Vector3 previousPosition;
	void Start () {
		previousPosition = transform.position;
	}

	void Update () {
		float offset = getOffset();
		if (offset != 0) {
			UpdateChildren (offset);
		}
	}

	private float getOffset() {
		return transform.parent.GetComponent<ScrollBackground> ().scrollRate;
	}

	protected abstract void UpdateChildren(float cameraOffset);



public abstract class InflictingDamage : MonoBehaviour {

	public int damage = 10;

}

public abstract class LevelEvent : MonoBehaviour {

	public bool running{
		get { return _running; }
	}
	private bool _running = false;

	public bool finished{
		get { return _finished; }
	}
	private bool _finished = false;

	public void LaunchEvent(){
		_running = true;
		StartCoroutine(EventActions ());
	}
		
	protected abstract IEnumerator EventActions ();

	public void FinishEvent(){
		StopCoroutine ("EventActions");
		_running = false;
		_finished = true;
	}
}






