using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	protected void UpdateParallax(Transform child, float cameraOffset) {
		//parallax is inversely proportional to zDistance. May be not strictly correct
		float zDistance = Mathf.Abs (child.position.z);
		float parallax = -cameraOffset / zDistance;
		//Debug.Log ("Z: " + zDistance + "\tparallax: " + parallax);
		child.Translate(new Vector3(parallax, 0, 0), Space.World);
	}
}

public abstract class InflictingDamage : MonoBehaviour {

	public int damage = 10;

}

public abstract class Event : MonoBehaviour {

	//event state fields
	public bool running{
		get { return _running; }
	}
	private bool _running = false;

	public bool finished{
		get { return _finished; }
	}
	private bool _finished = false;

	//event lifecycle
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

public abstract class LevelEvent : Event {
	[SerializeField]
	private ObjectPool enemyPool;
	private List<Transform> enemies = new List<Transform>();

	override protected IEnumerator EventActions() {
		yield return spawnEnemies ();
		yield return waitForLevelEnd ();
	}
		
	protected abstract IEnumerator spawnEnemies ();
	protected Transform spawnEnemy() {
		Transform result = enemyPool.GetInstance();
		enemies.Add (result);
		return result;
	}

	private IEnumerator waitForLevelEnd() {
		while (enemies.Count > 0) {
			for(int i=0; i<enemies.Count; i++) {
				Transform enemy = enemies[i];
				if (!enemy.gameObject.activeSelf) {
					enemies.Remove(enemy);
					i--;
				}
			}
			yield return new WaitForSeconds(1f);
		}
		FinishEvent();
	}
}






