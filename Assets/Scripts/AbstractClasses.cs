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

	protected void UpdateParallax(Transform child, float cameraOffset) {
		//parallax is inversely proportional to zDistance. May be not strictly correct
		float zDistance = Mathf.Abs (child.position.z);
		float parallax = -cameraOffset / zDistance;
		Debug.Log ("Z: " + zDistance + "\tparallax: " + parallax);
		child.Translate(new Vector3(parallax, 0, 0), Space.World);
	}
}